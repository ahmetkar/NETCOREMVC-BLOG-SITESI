'use client';
import { useEffect, useState } from 'react';
import { Form, Input, Button, Select, Upload, Card, Modal, Spin, App } from 'antd';
import { UploadOutlined, SaveOutlined, PictureOutlined } from '@ant-design/icons';
import { fetchAdminMakaleKategoriler, fetchAdminMakaleById, updateAdminMakale } from '@/services/admin/articleService';
import { fetchAdminMediaList } from '@/services/admin/mediaService';
import { CategoryDto, ArticleDto, MediaDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';
import { useRouter, useParams } from 'next/navigation';

const { TextArea } = Input;
const { Option } = Select;

export default function AdminUpdateArticle() {
  const { message } = App.useApp();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [fetching, setFetching] = useState(true);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [fileList, setFileList] = useState<any[]>([]);
  const router = useRouter();
  const { id } = useParams();

  // Media Modal states
  const [isMediaModalOpen, setIsMediaModalOpen] = useState(false);
  const [mediaList, setMediaList] = useState<MediaDto[]>([]);
  const [loadingMedia, setLoadingMedia] = useState(false);
  const [selectedMediaId, setSelectedMediaId] = useState<string | null>(null);
  const [selectedMediaUrl, setSelectedMediaUrl] = useState<string | null>(null);

  async function fetchCategories() {
    try {
      const data = await fetchAdminMakaleKategoriler();
      setCategories(data);
    } catch (error) {
      console.error(error);
      message.error('Kategoriler yüklenemedi.');
    }
  }

  async function fetchArticle() {
    try {
      const data = await fetchAdminMakaleById(id as string);
      
      form.setFieldsValue({
        title: data.title,
        categoryId: data.category?.id,
        description: data.description,
        content: data.content,
      });

      if (data.image) {
        setSelectedMediaId(data.image.id || null);
        setSelectedMediaUrl(getImageUrl(data.image.fileName) || '');
      }
    } catch (error) {
      console.error(error);
      message.error('Makale yüklenirken hata oluştu.');
    } finally {
      setFetching(false);
    }
  }

  useEffect(() => {
    fetchCategories();
    fetchArticle();
  }, [id]);

  async function openMediaModal() {
    setIsMediaModalOpen(true);
    if (mediaList.length === 0) {
      setLoadingMedia(true);
      try {
        const res = await fetchAdminMediaList();
        setMediaList(res);
      } catch (error) {
        console.error('Medyalar yüklenemedi', error);
        message.error('Resim galerisi yüklenemedi.');
      } finally {
        setLoadingMedia(false);
      }
    }
  }

  const handleMediaSelect = (media: MediaDto) => {
    setSelectedMediaId(media.id);
    setSelectedMediaUrl(getImageUrl(media.fileName) || '');
    setIsMediaModalOpen(false);
    setFileList([]); // Clear file upload if gallery image is selected
  };

  async function onFinish(values: any) {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append('Id', id as string);
      formData.append('Title', values.title);
      formData.append('Description', values.description);
      formData.append('Content', values.content);
      formData.append('CategoryId', values.categoryId);

      if (fileList.length > 0) {
        const file = fileList[0].originFileObj || fileList[0];
        formData.append('imageFile', file as Blob);
      } else if (selectedMediaId) {
        formData.append('SelectedImageId', selectedMediaId);
      }

      const res = await updateAdminMakale(formData);
      
      message.success(res.message || 'Makale başarıyla güncellendi.');
      router.push('/admin/articles');
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Makale güncellenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  const uploadProps = {
    onRemove: () => setFileList([]),
    beforeUpload: (file: any) => {
      setFileList([file]);
      // Clear gallery selection if a new file is uploaded
      setSelectedMediaId(null);
      setSelectedMediaUrl(null);
      return false; // Prevent automatic upload
    },
    fileList,
    maxCount: 1
  };

  return (
    <div className="max-w-4xl mx-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Makale Düzenle</h1>
        <Button onClick={() => router.push('/admin/articles')}>Geri Dön</Button>
      </div>

      <Card variant="borderless" className="shadow-sm">
        <Spin spinning={fetching}>
          <Form
            form={form}
            layout="vertical"
            onFinish={onFinish}
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

            <div className="mb-6 p-4 border border-dashed border-gray-300 rounded-lg bg-gray-50">
              <h3 className="text-lg font-medium mb-4">Kapak Fotoğrafı</h3>
              
              <div className="flex gap-4 items-end">
                <div className="flex-1">
                  <Form.Item label="Yeni Resim Yükle" className="mb-0">
                    <Upload {...uploadProps} accept="image/*">
                      <Button icon={<UploadOutlined />}>Bilgisayardan Seç</Button>
                    </Upload>
                  </Form.Item>
                </div>
                
                <div className="flex items-center pb-2">
                  <span className="text-gray-500 font-medium px-4">VEYA</span>
                </div>

                <div className="flex-1 pb-2">
                  <Button type="default" icon={<PictureOutlined />} onClick={openMediaModal} className="w-full">
                    Galeriden Seç
                  </Button>
                </div>
              </div>

              {selectedMediaUrl && !fileList.length && (
                <div className="mt-4">
                  <p className="text-sm text-gray-500 mb-2">Mevcut Seçili Resim:</p>
                  <img src={selectedMediaUrl} alt="Seçili Resim" className="h-32 object-cover rounded shadow-sm border border-gray-200" />
                </div>
              )}
            </div>

            <div className="flex justify-end mt-4 border-t pt-4">
              <Button type="primary" htmlType="submit" size="large" icon={<SaveOutlined />} loading={loading}>
                Değişiklikleri Kaydet
              </Button>
            </div>
          </Form>
        </Spin>
      </Card>

      <Modal
        title="Medyadan Resim Seç"
        open={isMediaModalOpen}
        onCancel={() => setIsMediaModalOpen(false)}
        footer={null}
        width={800}
      >
        <Spin spinning={loadingMedia}>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4 max-h-96 overflow-y-auto p-2">
            {mediaList.map((media) => (
              <div 
                key={media.id} 
                className={`cursor-pointer border-2 rounded-lg overflow-hidden transition-all duration-200 ${selectedMediaId === media.id ? 'border-blue-500 shadow-md scale-105' : 'border-transparent hover:border-blue-300'}`}
                onClick={() => handleMediaSelect(media)}
              >
                <img 
                  src={getImageUrl(media.fileName)}
                  alt={media.fileName}
                  className="w-full h-32 object-cover"
                />
              </div>
            ))}
            {mediaList.length === 0 && !loadingMedia && (
              <div className="col-span-4 text-center text-gray-500 py-8">Hiç resim bulunamadı.</div>
            )}
          </div>
        </Spin>
      </Modal>
    </div>
  );
}
