'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tooltip, Modal, Typography  } from 'antd';
import { DeleteOutlined, EyeOutlined } from '@ant-design/icons';
import { fetchAdminMesajlar, deleteAdminMesaj } from '@/services/admin/messageService';
import { MessageDto } from '@/types/dto';
import dayjs from 'dayjs';

const { Text, Paragraph } = Typography;

export default function AdminMessages() {
  const { message } = App.useApp();
  const [messagesList, setMessagesList] = useState<MessageDto[]>([]);
  const [loading, setLoading] = useState(true);
  
  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedMessage, setSelectedMessage] = useState<MessageDto | null>(null);

  async function fetchMessages() {
    setLoading(true);
    try {
      const data = await fetchAdminMesajlar();
      setMessagesList(data);
    } catch (error) {
      console.error(error);
      message.error('Mesajlar yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchMessages();
  }, []);

  const handleDelete = async (id: string) => {
    try {
      const res = await deleteAdminMesaj(id);
      message.success(res.message || 'Mesaj başarıyla silindi.');
      fetchMessages();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Mesaj silinirken hata oluştu.');
    }
  };

  const showMessageDetails = (msg: MessageDto) => {
    setSelectedMessage(msg);
    setIsModalOpen(true);
  };

  const columns = [
    {
      title: 'Gönderen',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => <strong>{text}</strong>,
    },
    {
      title: 'Konu',
      dataIndex: 'subject',
      key: 'subject',
    },
    {
      title: 'E-posta',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Tarih',
      dataIndex: 'createdDate',
      key: 'createdDate',
      render: (date: string) => dayjs(date).format('DD/MM/YYYY HH:mm'),
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: MessageDto) => (
        <Space size="middle">
          <Tooltip title="Mesajı Oku">
            <Button type="primary" icon={<EyeOutlined />} onClick={() => showMessageDetails(record)} />
          </Tooltip>
          <Popconfirm
            title="Emin misiniz?"
            description="Mesaj kalıcı olarak silinecektir."
            onConfirm={() => handleDelete(record.id || '')}
            okText="Evet"
            cancelText="Hayır"
          >
            <Tooltip title="Sil">
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
        <h1 className="text-2xl font-bold">Gelen Mesajlar</h1>
      </div>

      <Table 
        columns={columns} 
        dataSource={messagesList} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />

      <Modal
        title="Mesaj Detayı"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={[
          <Button key="close" onClick={() => setIsModalOpen(false)}>
            Kapat
          </Button>
        ]}
      >
        {selectedMessage && (
          <div className="space-y-4 mt-4">
            <div>
              <Text type="secondary">Gönderen</Text>
              <div className="font-semibold text-lg">{selectedMessage.name}</div>
            </div>
            <div>
              <Text type="secondary">E-posta</Text>
              <div><a href={`mailto:${selectedMessage.email}`}>{selectedMessage.email}</a></div>
            </div>
            <div>
              <Text type="secondary">Konu</Text>
              <div className="font-medium">{selectedMessage.subject}</div>
            </div>
            <div>
              <Text type="secondary">Mesaj İçeriği</Text>
              <Paragraph className="bg-gray-50 p-4 rounded-lg border mt-1">
                {selectedMessage.body || 'İçerik bulunamadı.'}
              </Paragraph>
            </div>
          </div>
        )}
      </Modal>
    </div>
  );
}
