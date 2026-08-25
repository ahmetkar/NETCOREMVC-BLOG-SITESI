import { Metadata } from 'next';
import BlogListClient from '@/components/public/BlogListClient';

export const dynamic = 'force-dynamic';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

async function getArticles() {
  try {
    const res = await fetch(`${API_URL}/Public/articles`, { cache: 'no-store' });
    if (!res.ok) throw new Error('API articles fetch failed');
    const articles = await res.json();
    return Array.isArray(articles) ? articles : (articles.articles || []);
  } catch (error) {
    console.error('Error fetching articles on server:', error);
    return [];
  }
}

export async function generateMetadata(): Promise<Metadata> {
  const title = 'Yayınlar ve Blog Yazıları | Blogger';
  const description = 'İlham veren makaleler, derinlemesine incelemeler ve teknoloji dünyasından en son haberler.';

  return {
    title,
    description,
    metadataBase: new URL(SITE_URL),
    alternates: {
      canonical: '/blog',
    },
    openGraph: {
      type: 'website',
      locale: 'tr_TR',
      url: `${SITE_URL}/blog`,
      title,
      description,
      siteName: 'Blogger',
    },
    twitter: {
      card: 'summary_large_image',
      title,
      description,
    },
  };
}

export default async function Page() {
  const articles = await getArticles();

  // Structured Data (JSON-LD Breadcrumb)
  const jsonLd = {
    '@context': 'https://schema.org',
    '@type': 'BreadcrumbList',
    'itemListElement': [
      {
        '@type': 'ListItem',
        'position': 1,
        'name': 'Ana Sayfa',
        'item': SITE_URL
      },
      {
        '@type': 'ListItem',
        'position': 2,
        'name': 'Blog',
        'item': `${SITE_URL}/blog`
      }
    ]
  };

  return (
    <>
      <script
        type="application/ld+json"
        dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }}
      />
      <BlogListClient initialArticles={articles} />
    </>
  );
}
