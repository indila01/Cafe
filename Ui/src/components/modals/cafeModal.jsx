import React from 'react';
import { Modal, Form, Input } from 'antd';

const CafeModal = ({ isVisible, isEditMode, onCancel, onSubmit, initialValues }) => {
  const [form] = Form.useForm();

  React.useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
    } else {
      form.resetFields();
    }
  }, [initialValues, form]);

  return (
    <Modal
      title={isEditMode ? "Edit Cafe" : "Add Cafe"}
      visible={isVisible}
      onCancel={onCancel}
      onOk={() => {
        form
          .validateFields()
          .then(values => {
            form.resetFields();
            onSubmit(values);
          })
          .catch(info => {
            console.log('Validate Failed:', info);
          });
      }}
    >
      <Form form={form} layout="vertical">
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