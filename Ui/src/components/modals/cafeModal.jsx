import React, { useState, useEffect } from 'react';
import { Modal, Form, Input } from 'antd';

const CafeModal = ({ isVisible, isEditMode, onCancel, onSubmit, initialValues }) => {
  const [form] = Form.useForm();
  const [isFormChanged, setIsFormChanged] = useState(false);

  useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
    } else {
      form.resetFields();
    }
  }, [initialValues, form]);

  useEffect(() => {
    const handleBeforeUnload = (e) => {
      if (isFormChanged) {
        e.preventDefault();
        e.returnValue = '';
      }
    };

    window.addEventListener('beforeunload', handleBeforeUnload);

    return () => {
      window.removeEventListener('beforeunload', handleBeforeUnload);
    };
  }, [isFormChanged]);

  const handleCancel = () => {
    if (isFormChanged) {
      const confirmClose = window.confirm("You have unsaved changes. Are you sure you want to close?");
      if (confirmClose) {
        onCancel();
      }
    } else {
      onCancel();
    }
  };

  return (
    <Modal
      title={isEditMode ? "Edit Cafe" : "Add Cafe"}
      visible={isVisible}
      onCancel={handleCancel}
      onOk={() => {
        form
          .validateFields()
          .then(values => {
            form.resetFields();
            onSubmit(values);
            setIsFormChanged(false);
          })
          .catch(info => {
            console.log('Validate Failed:', info);
          });
      }}
    >
      <Form
        form={form}
        layout="vertical"
        onValuesChange={() => setIsFormChanged(true)}
      >
        <Form.Item name="name" label="Name" rules={[{ required: true, message: 'Please input the name!' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="description" label="Description" rules={[{ required: true, message: 'Please input the description!' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="location" label="Location" rules={[{ required: true, message: 'Please input the location!' }]}>
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CafeModal;