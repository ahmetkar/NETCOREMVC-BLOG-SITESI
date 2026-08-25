import axiosInstance from '@/lib/axios';
import { CategoryDto, ArticleDto, LayoutDataDto, ContactRequestDto } from '@/types/dto';

export async function fetchKategoriler(): Promise<CategoryDto[]> {
  const res = await axiosInstance.get('/Public/categories');
  return res.data;
}

export async function fetchKategoriDetay(slug: string): Promise<{ category: CategoryDto; articles: { articles: ArticleDto[] } | ArticleDto[] }> {
  const res = await axiosInstance.get(`/Public/category/${slug}`);
  return res.data;
}

export async function fetchKategorilerLazy(skip = 0, take = 2): Promise<{ success: boolean; categories: { id: string; name: string; slug: string; articles: ArticleDto[] }[] }> {
  const res = await axiosInstance.get(`/Public/categories-lazy?skip=${skip}&take=2`);
  return res.data;
}

