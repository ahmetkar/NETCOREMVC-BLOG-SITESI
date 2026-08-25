import axiosInstance from '@/lib/axios';

export async function fetchAdminDashboardToplamMakale(): Promise<number> {
  const res = await axiosInstance.get('/admin/Dashboard/total-articles');
  return res.data;
}

export async function fetchAdminDashboardToplamGoruntulenme(): Promise<number> {
  const res = await axiosInstance.get('/admin/Dashboard/total-views');
  return res.data;
}

