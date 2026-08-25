'use client';
import { useEffect, useState } from 'react';
import { Card, Col, Row, Statistic, Spin, App } from 'antd';
import { FileTextOutlined, EyeOutlined, LineChartOutlined } from '@ant-design/icons';
import { fetchAdminDashboardToplamMakale, fetchAdminDashboardToplamGoruntulenme } from '@/services/admin/dashboardService';

export default function AdminDashboard() {
  const { message } = App.useApp();
  const [loading, setLoading] = useState(true);
  const [totalArticles, setTotalArticles] = useState(0);
  const [totalViews, setTotalViews] = useState(0);
  // const [yearlyCounts, setYearlyCounts] = useState([]);

  async function fetchDashboardData() {
    try {
      const [articlesCount, viewsCount] = await Promise.all([
        fetchAdminDashboardToplamMakale(),
        fetchAdminDashboardToplamGoruntulenme(),
      ]);

      setTotalArticles(articlesCount);
      setTotalViews(viewsCount);
    } catch (error: any) {
      console.error(error);
      if (error.response?.status !== 403) {
        message.error('Dashboard verileri alınırken bir hata oluştu.');
      }
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchDashboardData();
  }, []);

  if (loading) {
    return <div className="flex justify-center items-center h-64"><Spin size="large" /></div>;
  }

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">Dashboard</h1>
      <Row gutter={16}>
        <Col span={8}>
          <Card variant="borderless">
            <Statistic
              title="Toplam Makale"
              value={totalArticles}
              prefix={<FileTextOutlined className="text-blue-500" />}
            />
          </Card>
        </Col>
        <Col span={8}>
          <Card variant="borderless">
            <Statistic
              title="Toplam Görüntülenme"
              value={totalViews}
              prefix={<EyeOutlined className="text-green-500" />}
            />
          </Card>
        </Col>
        <Col span={8}>
          <Card variant="borderless">
            <Statistic
              title="Aylık Büyüme (Örnek)"
              value={15}
              suffix="%"
              prefix={<LineChartOutlined className="text-red-500" />}
            />
          </Card>
        </Col>
      </Row>
      
      {/* İleride Grafik Eklenebilir */}
      <div className="mt-8">
        <Card title="Yıllık Makale Dağılımı" variant="borderless">
          <div className="h-64 flex items-center justify-center text-gray-400 bg-gray-50 rounded">
            Grafik Alanı (Recharts / Chart.js kullanılabilir)
          </div>
        </Card>
      </div>
    </div>
  );
}
