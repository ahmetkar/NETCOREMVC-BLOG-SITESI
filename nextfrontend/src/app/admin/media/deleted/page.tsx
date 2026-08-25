'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tooltip, Tag  } from 'antd';
import { DeleteOutlined, RollbackOutlined, EyeOutlined } from '@ant-design/icons';
import { fetchAdminDeletedMedia, forceDeleteAdminMedia } from '@/services/admin/mediaService';
import { MediaDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';
import dayjs from 'dayjs';
import Link from 'next/link';

export default function AdminMediaDeleted() {
  const { message } = App.useApp();
  const [medias, setMedias] = useState<MediaDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchDeletedMedias() {
    setLoading(true);
    try {
      const data = await fetchAdminDeletedMedia();
      setMedias(data);
    } catch (error) {
      console.error(error);
      message.error('Silinen medyalar yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchDeletedMedias();
  }, []);

  const handleForceDelete = async (id: string) => {
    try {
      const res = await forceDeleteAdminMedia(id);
      message.success(res.message || 'Resim kalıcı olarak silindi.');
      fetchDeletedMedias();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Silme işlemi başarısız.');
    }
  };

  const columns = [
    {
      title: 'Resim',
      dataIndex: 'fileName',
      key: 'image',
      render: (fileName: string) => (
        <img 
          src={getImageUrl(fileName)}
          alt={fileName} 
          style={{ width: '80px', height: '80px', objectFit: 'cover', borderRadius: '8px', opacity: 0.6 }} 
        />
      ),
    },
    {
      title: 'Dosya Adı',
      dataIndex: 'fileName',
      key: 'fileName',
    },
    {
      title: 'Silinme Tarihi',
      dataIndex: 'deleteDate',
      key: 'deleteDate',
      render: (date: string) => <Tag color="red">{dayjs(date).format('DD/MM/YYYY HH:mm')}</Tag>,
    },
    {
      title: 'Silen Kullanıcı',
      dataIndex: 'deletedBy',
      key: 'deletedBy',
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: MediaDto) => (
        <Space size="middle">
          <Popconfirm
            title="Emin misiniz?"
            description="Bu resim geri döndürülemez şekilde kalıcı olarak silinecektir!"
            onConfirm={() => handleForceDelete(record.id || '')}
            okText="Kalıcı Sil"
            cancelText="İptal"
            okButtonProps={{ danger: true }}
          >
            <Tooltip title="Kalıcı Sil">
              <Button danger icon={<DeleteOutlined />} />
            </Tooltip>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-red-600">Silinen Medyalar</h1>
        <Link href="/admin/media">
          <Button type="primary" icon={<RollbackOutlined />}>
            Medyalara Dön
          </Button>
        </Link>
      </div>

      <Table 
        columns={columns} 
        dataSource={medias} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />
    </div>
  );
}
