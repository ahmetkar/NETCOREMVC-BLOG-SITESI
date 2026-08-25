'use client';
import { useEffect, useState } from 'react';
import { App,  Form, Input, Button, Upload, Card, Avatar  } from 'antd';
import { UploadOutlined, UserOutlined } from '@ant-design/icons';
import { fetchAdminProfil, updateAdminProfil } from '@/services/admin/settingSubscriberService';
import { UserDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';

export default function AdminProfile() {
  const { message } = App.useApp();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [fetching, setFetching] = useState(true);
  const [fileList, setFileList] = useState<any[]>([]);
  const [profileData, setProfileData] = useState<any>(null);

  useEffect(() => {
    if (profileData && !fetching) {
      form.setFieldsValue({
        firstName: profileData.firstName,
        lastName: profileData.lastName,
        userName: profileData.userName,
        email: profileData.email,
        phoneNumber: profileData.phoneNumber,
        biography: profileData.biography,
      });
    }
  }, [profileData, fetching, form]);

  async function fetchProfile() {
    try {
      const data = await fetchAdminProfil();
      setProfileData(data);
    } catch (error) {
      console.error(error);
      message.error('Profil bilgileri yüklenemedi.');
    } finally {
      setFetching(false);
    }
  }

  useEffect(() => {
    fetchProfile();
  }, []);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append('firstName', values.firstName);
      formData.append('lastName', values.lastName);
      formData.append('userName', values.userName);
      formData.append('email', values.email);
      if (values.phoneNumber) formData.append('phoneNumber', values.phoneNumber);
      if (values.biography) formData.append('biography', values.biography);
      if (values.currentPassword) formData.append('currentPassword', values.currentPassword);
      if (values.newPassword) formData.append('newPassword', values.newPassword);
      if (profileData?.currentImage) formData.append('currentImage', profileData.currentImage);
      
      if (fileList.length > 0) {
        const file = fileList[0].originFileObj || fileList[0];
        if (file) {
          formData.append('photo', file as Blob);
        }
      }

      const res = await updateAdminProfil(formData);
      
      message.success(res?.message || 'Profil güncellendi.');
      if(values.newPassword) {
         form.setFieldsValue({ currentPassword: '', newPassword: '' });
      }
      // Re-fetch profile to update image and details
      fetchProfile();
      setFileList([]);
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Profil güncellenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  };

  const uploadProps = {
    onRemove: () => {
      setFileList([]);
    },
    beforeUpload: (file: any) => {
      setFileList([file]);
      return false; // Prevent automatic upload
    },
    fileList,
    maxCount: 1
  };

  if (fetching) {
    return (
      <div className="max-w-2xl mx-auto">
        <h1 className="text-2xl font-bold mb-6">Profilim</h1>
        <Card loading={true} className="shadow-sm h-96"></Card>
      </div>
    );
  }

  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-6">Profilim</h1>
      
      <Card className="shadow-sm">
        <div className="flex flex-col items-center mb-8">
          <Avatar 
            size={100} 
            icon={<UserOutlined />} 
            src={getImageUrl(profileData?.currentImage)}
            className="mb-4 bg-blue-500" 
          />
          <h2 className="text-xl font-semibold">{form.getFieldValue('firstName') || profileData?.firstName} {form.getFieldValue('lastName') || profileData?.lastName}</h2>
          <p className="text-gray-500">@{form.getFieldValue('userName') || profileData?.userName}</p>
        </div>

        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
        >
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Form.Item name="firstName" label="Ad" rules={[{ required: true, message: 'Lütfen ad giriniz' }]}>
              <Input size="large" />
            </Form.Item>
            <Form.Item name="lastName" label="Soyad" rules={[{ required: true, message: 'Lütfen soyad giriniz' }]}>
              <Input size="large" />
            </Form.Item>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Form.Item name="userName" label="Kullanıcı Adı" rules={[{ required: true, message: 'Lütfen kullanıcı adı giriniz' }]}>
              <Input size="large" />
            </Form.Item>
            <Form.Item name="email" label="E-posta" rules={[{ required: true, type: 'email', message: 'Geçerli e-posta giriniz' }]}>
              <Input size="large" />
            </Form.Item>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Form.Item name="phoneNumber" label="Telefon Numarası">
              <Input size="large" />
            </Form.Item>
          </div>
          
          <Form.Item name="biography" label="Biyografi">
            <Input.TextArea rows={4} placeholder="Kısaca kendinizden bahsedin..." />
          </Form.Item>

          <div className="border-t pt-6 mt-2">
            <h3 className="text-lg font-medium mb-4">Şifre Değiştir (İsteğe Bağlı)</h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <Form.Item name="currentPassword" label="Mevcut Şifre">
                <Input.Password size="large" />
              </Form.Item>
              <Form.Item name="newPassword" label="Yeni Şifre">
                <Input.Password size="large" />
              </Form.Item>
            </div>
          </div>

          <div className="border-t pt-6 mt-2">
            <h3 className="text-lg font-medium mb-4">Profil Fotoğrafı</h3>
            <Form.Item label="Fotoğraf Yükle">
              <Upload {...uploadProps} accept="image/*">
                <Button icon={<UploadOutlined />}>Resim Seç</Button>
              </Upload>
            </Form.Item>
          </div>

          <Form.Item className="mt-8 mb-0">
            <Button type="primary" htmlType="submit" size="large" loading={loading} className="w-full">
              Profili Güncelle
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
}
