'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tooltip  } from 'antd';
import { DeleteOutlined, RollbackOutlined, EyeOutlined } from '@ant-design/icons';
import { fetchAdminMediaList, deleteAdminMedia } from '@/services/admin/mediaService';
import { MediaDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';
import dayjs from 'dayjs';
import Link from 'next/link';

export default function AdminMedia() {
  const { message } = App.useApp();
  const [medias, setMedias] = useState<MediaDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchMedias() {
    setLoading(true);
    try {
      const data = await fetchAdminMediaList();
      setMedias(data);
    } catch (error) {
      console.error(error);
      message.error('Medyalar yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchMedias();
  }, []);

  const handleSafeDelete = async (id: string) => {
    try {
      const res = await deleteAdminMedia(id);
      message.success(res.message || 'Resim çöp kutusuna taşındı.');
      fetchMedias();
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
          style={{ width: '80px', height: '80px', objectFit: 'cover', borderRadius: '8px' }} 
        />
      ),
    },
    {
      title: 'Dosya Adı',
      dataIndex: 'fileName',
      key: 'fileName',
    },
    {
      title: 'Dosya Türü',
      dataIndex: 'fileType',
      key: 'fileType',
    },
    {
      title: 'Yüklenme Tarihi',
      dataIndex: 'createdDate',
      key: 'createdDate',
      render: (date: string) => dayjs(date).format('DD/MM/YYYY HH:mm'),
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: MediaDto) => (
        <Space size="middle">
          <Tooltip title="Görüntüle">
            <a href={getImageUrl(record.fileName)} target="_blank" rel="noreferrer">
              <Button type="primary" icon={<EyeOutlined />} />
            </a>
          </Tooltip>
          <Popconfirm
            title="Emin misiniz?"
            description="Resim çöp kutusuna taşınacaktır."
            onConfirm={() => handleSafeDelete(record.id)}
            okText="Evet"
            cancelText="Hayır"
          >
            <Tooltip title="Çöp Kutusuna Taşı">
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
        <h1 className="text-2xl font-bold">Medya Kütüphanesi</h1>
        <Link href="/admin/media/deleted">
          <Button type="default" icon={<RollbackOutlined />}>
            Çöp Kutusu
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
