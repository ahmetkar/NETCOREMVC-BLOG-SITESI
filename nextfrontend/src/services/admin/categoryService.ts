import axiosInstance from '@/lib/axios';
import { CategoryDto } from '@/types/dto';

export async function fetchAdminKategoriler(): Promise<CategoryDto[]> {
  const res = await axiosInstance.get('/admin/Category');
  return res.data;
}

export async function createAdminKategori(data: { name: string }): Promise<{ message?: string }> {
  const res = await axiosInstance.post('/admin/Category', data);
  return res.data;
}

export async function updateAdminKategori(id: string, data: { name: string; isDeleted?: boolean }): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Category/${id}`, data);
  return res.data;
}

export async function deleteAdminKategori(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Category/${id}`);
  return res.data;
}

export async function fetchAdminCopKategoriler(): Promise<CategoryDto[]> {
  const res = await axiosInstance.get('/admin/Category/deleted');
  return res.data;
}

export async function undoDeleteAdminKategori(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Category/undo/${id}`);
  return res.data;
}

export async function forceDeleteAdminKategori(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Category/force/${id}`);
  return res.data;
}

