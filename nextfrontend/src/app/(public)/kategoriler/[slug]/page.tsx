import { Metadata } from 'next';
import CategoryDetailClient from '@/components/public/CategoryDetailClient';
import Link from 'next/link';

export const dynamic = 'force-dynamic';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

async function getCategoryData(slug: string) {
  try {
    const res = await fetch(`${API_URL}/Public/category/${slug}`, { cache: 'no-store' });
    if (!res.ok) throw new Error('API category fetch failed');
    return await res.json();
  } catch (error) {
    console.error(`Error fetching category ${slug} on server:`, error);
    return null;
  }
}

export async function generateMetadata({ params }: { params: Promise<{ slug: string }> }): Promise<Metadata> {
  const { slug } = await params;
  const data = await getCategoryData(slug);
  
  if (!data || !data.category) {
    return {
      title: 'Kategori Bulunamadı | Blogger',
    };
  }

  const category = data.category;
  const title = `${category.name} Kategorisindeki Yazılar | Blogger`;
  const description = `${category.name} kategorisinde yayınlanmış en güncel blog yazıları ve haberler.`;

  return {
    title,
    description,
    metadataBase: new URL(SITE_URL),
    alternates: {
      canonical: `/kategoriler/${slug}`,
    },
    openGraph: {
      type: 'website',
      locale: 'tr_TR',
      url: `${SITE_URL}/kategoriler/${slug}`,
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

export default async function Page({ params }: { params: Promise<{ slug: string }> }) {
  const { slug } = await params;
  const data = await getCategoryData(slug);
  
  if (!data || !data.category) {
    return (
      <div className="min-h-[70vh] flex flex-col items-center justify-center text-center px-4">
         <h1 className="text-6xl font-display font-black text-brand-text mb-6">404</h1>
         <p className="text-xl text-brand-muted mb-8">Aradığınız kategori bulunamadı.</p>
         <Link href="/blog" className="px-8 py-3 bg-brand-primary text-white text-xs font-bold uppercase tracking-widest rounded-full hover:bg-brand-primary/90 transition-colors">
           Blog&apos;a Dön
         </Link>
      </div>
    );
  }

  const category = data.category;
  const rawArticles = data.articles?.articles || data.articles || [];
  const articles = Array.isArray(rawArticles) ? rawArticles : (rawArticles.articles ? rawArticles.articles : []);

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
        'name': category.name,
        'item': `${SITE_URL}/kategoriler/${slug}`
      }
    ]
  };

  return (
    <>
      <script
        type="application/ld+json"
        dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }}
      />
      <CategoryDetailClient category={category} articles={articles} />
    </>
  );
}
