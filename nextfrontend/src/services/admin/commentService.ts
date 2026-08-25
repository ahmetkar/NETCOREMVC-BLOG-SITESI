import axiosInstance from '@/lib/axios';
import { CommentDto } from '@/types/dto';

export async function fetchAdminYorumlar(): Promise<CommentDto[]> {
  const res = await axiosInstance.get('/admin/Article/comments');
  return res.data;
}

export async function deleteAdminYorum(articleId: string, commentId: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Article/comment/${articleId}/${commentId}`);
  return res.data;
}

export async function updateYorumOnay(articleId: string, commentId: string, approved: boolean): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Article/comment/${articleId}/${commentId}?approved=${approved}`);
  return res.data;
}

