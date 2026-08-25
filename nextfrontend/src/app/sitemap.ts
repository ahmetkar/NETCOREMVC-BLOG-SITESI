import { MetadataRoute } from 'next';

export const revalidate = 3600; // Cache and revalidate every 1 hour

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

export default async function sitemap(): Promise<MetadataRoute.Sitemap> {
  const sitemapItems: MetadataRoute.Sitemap = [];

  // 1. Static Pages
  const staticPages = [
    { 
      url: SITE_URL, 
      lastModified: new Date(), 
      changeFrequency: 'daily' as const, 
      priority: 1.0 
    },
    { 
      url: `${SITE_URL}/blog`, 
      lastModified: new Date(), 
      changeFrequency: 'daily' as const, 
      priority: 0.9 
    },
    { 
      url: `${SITE_URL}/kategoriler`, 
      lastModified: new Date(), 
      changeFrequency: 'weekly' as const, 
      priority: 0.8 
    },
    { 
      url: `${SITE_URL}/hakkimizda`, 
      lastModified: new Date(), 
      changeFrequency: 'monthly' as const, 
      priority: 0.5 
    },
    { 
      url: `${SITE_URL}/iletisim`, 
      lastModified: new Date(), 
      changeFrequency: 'monthly' as const, 
      priority: 0.5 
    },
  ];
  sitemapItems.push(...staticPages);

  // 2. Dynamic Article Pages
  try {
    const articlesRes = await fetch(`${API_URL}/Public/articles`, { next: { revalidate: 3600 } });
    if (articlesRes.ok) {
      const articles = await articlesRes.json();
      const articleList = Array.isArray(articles) ? articles : (articles.articles || []);
      
      articleList.forEach((article: any) => {
        const slugOrId = article.slug || article.id;
        sitemapItems.push({
          url: `${SITE_URL}/blog/${slugOrId}`,
          lastModified: new Date(article.modifiedDate || article.createdDate),
          changeFrequency: 'weekly' as const,
          priority: 0.7,
        });
      });
    }
  } catch (error) {
    console.error('Error fetching articles for sitemap:', error);
  }

  // 3. Dynamic Category Pages
  try {
    const categoriesRes = await fetch(`${API_URL}/Public/categories`, { next: { revalidate: 3600 } });
    if (categoriesRes.ok) {
      const categories = await categoriesRes.json();
      categories.forEach((category: any) => {
        const slugOrId = category.slug || category.id;
        sitemapItems.push({
          url: `${SITE_URL}/kategoriler/${slugOrId}`,
          lastModified: new Date(),
          changeFrequency: 'weekly' as const,
          priority: 0.6,
        });
      });
    }
  } catch (error) {
    console.error('Error fetching categories for sitemap:', error);
  }

  return sitemapItems;
}
