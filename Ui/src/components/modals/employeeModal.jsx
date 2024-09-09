import { useState, useEffect } from 'react';
import { Modal, Form, Input, Radio, Select, Button } from 'antd';

const { Option } = Select;

const EmployeeModal = ({ isVisible, isEditMode, onCancel, onSubmit, initialValues, cafes }) => {
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
      title={isEditMode ? "Edit Employee" : "Add Employee"}
      visible={isVisible}
      onCancel={handleCancel}
      onOk={() => {
        form
          .validateFields()
          .then(values => {
            form.resetFields();
            onSubmit({ ...values, cafeId: values.cafe });
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
        <Form.Item
          name="name"
          label="Name"
          rules={[
            { required: true, message: 'Please input the name!' },
            { min: 6, message: 'Name must be at least 6 characters' },
            { max: 10, message: 'Name must be at most 10 characters' }
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="email"
          label="Email"
          rules={[
            { required: true, message: 'Please input the email!' },
            { type: 'email', message: 'Please enter a valid email!' }
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="phoneNumber"
          label="Phone Number"
          rules={[
            { required: true, message: 'Please input the phone number!' },
            { pattern: /^[0-9]+$/, message: 'Phone number must be numeric!' }
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="gender"
          label="Gender"
          rules={[{ required: true, message: 'Please select the gender!' }]}
        >
          <Radio.Group>
            <Radio value="Male">Male</Radio>
            <Radio value="Female">Female</Radio>
          </Radio.Group>
        </Form.Item>
        <Form.Item
          name="cafe"
          label="Assigned Cafe"
          rules={[{ required: false, message: 'Please select the assigned cafe!' }]}
        >
          <Select placeholder="Select a cafe">
            {cafes.map(cafe => (
              <Option key={cafe.id} value={cafe.id}>{cafe.name}</Option>
            ))}
          </Select>
         
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default EmployeeModal;