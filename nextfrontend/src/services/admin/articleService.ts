import axiosInstance from '@/lib/axios';
import { ArticleDto, CategoryDto } from '@/types/dto';

export async function fetchAdminMakaleler(): Promise<ArticleDto[]> {
  const res = await axiosInstance.get('/admin/Article');
  return res.data;
}

export async function fetchAdminMakaleKategoriler(): Promise<CategoryDto[]> {
  const res = await axiosInstance.get('/admin/Article/categories');
  return res.data;
}

export async function createAdminMakale(formData: FormData, aiActive: boolean): Promise<{ message?: string }> {
  const res = await axiosInstance.post(`/admin/Article?isAIActive=${aiActive}`, formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

export async function deleteAdminMakale(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Article/${id}`);
  return res.data;
}

export async function fetchAdminCopMakaleler(): Promise<ArticleDto[]> {
  const res = await axiosInstance.get('/admin/Article/deleted');
  return res.data;
}

export async function undoDeleteAdminMakale(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Article/undo/${id}`);
  return res.data;
}

export async function forceDeleteAdminMakale(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Article/force/${id}`);
  return res.data;
}

export async function fetchAdminMakaleById(id: string): Promise<ArticleDto> {
  const res = await axiosInstance.get(`/admin/Article/${id}`);
  return res.data;
}

export async function updateAdminMakale(formData: FormData): Promise<{ message?: string }> {
  const res = await axiosInstance.put('/admin/Article', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

