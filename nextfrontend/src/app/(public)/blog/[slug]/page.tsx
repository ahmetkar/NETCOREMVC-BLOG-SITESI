import { Metadata } from 'next';
import ArticleDetailClient from '@/components/public/ArticleDetailClient';
import { getImageUrl } from '@/lib/image-url';
import Link from 'next/link';

export const dynamic = 'force-dynamic';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

async function getArticleData(slug: string) {
  try {
    const res = await fetch(`${API_URL}/Public/blog/${slug}`, { cache: 'no-store' });
    if (!res.ok) throw new Error('API blog fetch failed');
    return await res.json();
  } catch (error) {
    console.error(`Error fetching article ${slug} on server:`, error);
    return null;
  }
}

export async function generateMetadata({ params }: { params: Promise<{ slug: string }> }): Promise<Metadata> {
  const { slug } = await params;
  const data = await getArticleData(slug);
  
  if (!data || !data.article) {
    return {
      title: 'Makale Bulunamadı | Blogger',
    };
  }

  const article = data.article;
  const title = `${article.title} | Blogger`;
  const plainTextContent = article.content ? article.content.replace(/<[^>]+>/g, '') : '';
  const description = plainTextContent.substring(0, 155).trim() + '...';
  const imageUrl = getImageUrl(article.image?.fileName, 'https://via.placeholder.com/1200x630') || 'https://via.placeholder.com/1200x630';

  return {
    title,
    description,
    metadataBase: new URL(SITE_URL),
    alternates: {
      canonical: `/blog/${slug}`,
    },
    openGraph: {
      type: 'article',
      locale: 'tr_TR',
      url: `${SITE_URL}/blog/${slug}`,
      title,
      description,
      siteName: 'Blogger',
      publishedTime: article.createdDate,
      modifiedTime: article.modifiedDate || article.createdDate,
      authors: [`${article.user?.firstName} ${article.user?.lastName}`],
      tags: [article.category?.name || 'Blog'],
      images: [
        {
          url: imageUrl,
          width: 1200,
          height: 630,
          alt: article.title,
        }
      ],
    },
    twitter: {
      card: 'summary_large_image',
      title,
      description,
      images: [imageUrl],
    },
  };
}

export default async function Page({ params }: { params: Promise<{ slug: string }> }) {
  const { slug } = await params;
  const data = await getArticleData(slug);
  
  if (!data || !data.article) {
    return (
      <div className="min-h-[70vh] flex flex-col items-center justify-center text-center px-4">
         <h1 className="text-6xl font-display font-black text-brand-text mb-6">404</h1>
         <p className="text-xl text-brand-muted mb-8">Aradığınız makale bulunamadı.</p>
         <Link href="/blog" className="px-8 py-3 bg-brand-primary text-white text-xs font-bold uppercase tracking-widest rounded-full hover:bg-brand-primary/90 transition-colors">
            Bloga Dön
         </Link>
      </div>
    );
  }

  const article = data.article;
  const plainTextContent = article.content ? article.content.replace(/<[^>]+>/g, '') : '';
  const description = plainTextContent.substring(0, 155).trim() + '...';
  const imageUrl = getImageUrl(article.image?.fileName, 'https://via.placeholder.com/1200x630') || 'https://via.placeholder.com/1200x630';

  // Schema.org JSON-LD structured data
  const jsonLd = {
    '@context': 'https://schema.org',
    '@type': 'BlogPosting',
    'headline': article.title,
    'image': [imageUrl],
    'datePublished': article.createdDate,
    'dateModified': article.modifiedDate || article.createdDate,
    'author': [{
      '@type': 'Person',
      'name': `${article.user?.firstName} ${article.user?.lastName}`,
      'url': `${SITE_URL}/author/${article.user?.userName || article.user?.id || ''}`
    }],
    'publisher': {
      '@type': 'Organization',
      'name': 'Blogger',
      'logo': {
        '@type': 'ImageObject',
        'url': `${SITE_URL}/favicon.ico`
      }
    },
    'description': description,
    'mainEntityOfPage': {
      '@type': 'WebPage',
      '@id': `${SITE_URL}/blog/${slug}`
    }
  };

  return (
    <>
      <script
        type="application/ld+json"
        dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }}
      />
      <ArticleDetailClient article={article} mayLikeArticles={data.MayLikeArticles || data.mayLikeArticles || []} />
    </>
  );
}
