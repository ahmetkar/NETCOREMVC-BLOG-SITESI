'use client';
import { useEffect, useState } from 'react';
import { Form, Input, Button, Select, Upload, Card, Switch, App } from 'antd';
import { UploadOutlined, SaveOutlined } from '@ant-design/icons';
import { fetchAdminMakaleKategoriler, createAdminMakale } from '@/services/admin/articleService';
import { CategoryDto } from '@/types/dto';
import { useRouter } from 'next/navigation';

const { TextArea } = Input;
const { Option } = Select;

export default function AdminAddArticle() {
  const { message } = App.useApp();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [fileList, setFileList] = useState<any[]>([]);
  const router = useRouter();

  async function fetchCategories() {
    try {
      const data = await fetchAdminMakaleKategoriler();
      setCategories(data);
    } catch (error) {
      console.error(error);
      message.error('Kategoriler yüklenemedi.');
    }
  }

  useEffect(() => {
    fetchCategories();
  }, []);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append('Title', values.title);
      formData.append('Description', values.description);
      formData.append('Content', values.content);
      formData.append('CategoryId', values.categoryId);
      
      const aiActive = values.isAIActive ? true : false;

      if (fileList.length > 0) {
        const file = fileList[0].originFileObj || fileList[0];
        formData.append('imageFile', file as Blob);
      }

      const res = await createAdminMakale(formData, aiActive);
      
      message.success(res.message || 'Makale başarıyla eklendi.');
      router.push('/admin/articles');
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Makale eklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  };

  const uploadProps = {
    onRemove: () => setFileList([]),
    beforeUpload: (file: any) => {
      setFileList([file]);
      return false; // Prevent automatic upload
    },
    fileList,
    maxCount: 1
  };

  return (
    <div className="max-w-4xl mx-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Yeni Makale Ekle</h1>
        <Button onClick={() => router.push('/admin/articles')}>Geri Dön</Button>
      </div>

      <Card className="shadow-sm">
        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
          initialValues={{ isAIActive: true }}
        >
          <Form.Item name="title" label="Başlık" rules={[{ required: true, message: 'Lütfen başlık giriniz' }]}>
            <Input size="large" />
          </Form.Item>

          <Form.Item name="categoryId" label="Kategori" rules={[{ required: true, message: 'Lütfen kategori seçiniz' }]}>
            <Select size="large" placeholder="Kategori Seçin">
              {categories.map((cat: CategoryDto) => (
                <Option key={cat.id} value={cat.id}>{cat.name}</Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="description" label="Kısa Açıklama" rules={[{ required: true, message: 'Lütfen açıklama giriniz' }]}>
            <TextArea rows={3} />
          </Form.Item>

          <Form.Item name="content" label="İçerik" rules={[{ required: true, message: 'Lütfen içerik giriniz' }]}>
            <TextArea rows={12} placeholder="Makale içeriğini buraya yazın..." />
          </Form.Item>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Form.Item label="Kapak Fotoğrafı">
              <Upload {...uploadProps} accept="image/*">
                <Button icon={<UploadOutlined />}>Resim Seç</Button>
              </Upload>
            </Form.Item>

            <Form.Item name="isAIActive" label="Yapay Zeka SEO Desteği" valuePropName="checked">
              <Switch checkedChildren="Aktif" unCheckedChildren="Pasif" />
            </Form.Item>
          </div>

          <div className="flex justify-end mt-4 border-t pt-4">
            <Button type="primary" htmlType="submit" size="large" icon={<SaveOutlined />} loading={loading}>
              Makaleyi Kaydet
            </Button>
          </div>
        </Form>
      </Card>
    </div>
  );
}
