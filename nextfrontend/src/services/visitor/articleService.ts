import axiosInstance from '@/lib/axios';
import { ArticleDto } from '@/types/dto';

export async function fetchMakaleler(): Promise<ArticleDto[]> {
  const res = await axiosInstance.get('/Public/articles');
  return res.data;
}

export async function fetchMakaleDetay(slug: string): Promise<{ article: ArticleDto; mayLikeArticles?: ArticleDto[]; MayLikeArticles?: ArticleDto[] }> {
  const res = await axiosInstance.get(`/Public/blog/${slug}`);
  return res.data;
}

export async function fetchArama(keyword: string): Promise<{ articles: ArticleDto[] } | ArticleDto[]> {
  const res = await axiosInstance.get(`/Public/search?keyword=${encodeURIComponent(keyword)}`);
  return res.data;
}
export async function fetchMakalelerMinimal(): Promise<ArticleDto[]> {
  const res = await axiosInstance.get('/Public/articles-minimal');
  return res.data;
}


