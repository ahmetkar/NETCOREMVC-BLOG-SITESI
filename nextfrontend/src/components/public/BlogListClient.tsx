'use client';
import { useState } from 'react';
import Link from 'next/link';
import { getImageUrl } from '@/lib/image-url';

export default function BlogListClient({ initialArticles }: { initialArticles: any[] }) {
  const [articles] = useState<any[]>(initialArticles || []);

  const sortedArticles = [...articles].sort(
    (a: any, b: any) => new Date(b.createdDate).getTime() - new Date(a.createdDate).getTime()
  );

  return (
    <div className="w-full relative min-h-screen">
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[5%] right-[10%] w-[40%] h-[40%] rounded-full bg-brand-primary/10 blur-[150px] pointer-events-none -z-10"></div>
      
      {/* Header */}
      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 py-16 lg:py-24 text-center border-b border-brand-border/30 mb-16">
        <span className="text-[11px] font-bold uppercase tracking-widest text-brand-primary mb-6 block">Tüm Yayınlar</span>
        <h1 className="text-5xl sm:text-7xl lg:text-8xl font-display font-black text-brand-text mb-8 tracking-tighter">
          Blog.
        </h1>
        <p className="text-xl text-brand-muted font-medium max-w-2xl mx-auto">
          İlham veren makaleler, derinlemesine incelemeler ve teknoloji dünyasından en son haberler.
        </p>
      </div>

      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 pb-32">
        {sortedArticles.length === 0 ? (
          <div className="text-center py-20">
            <span className="text-brand-muted font-medium text-lg">Yayınlanmış makale bulunamadı.</span>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {sortedArticles.map((article: any) => (
              <div key={article.id} className="glass rounded-3xl overflow-hidden hover:shadow-2xl hover:shadow-brand-primary/10 transition-all duration-500 transform hover:-translate-y-2 border border-white/40 group">
                <Link href={`/blog/${article.slug || article.id}`} className="block h-full flex flex-col">
                  <div className="w-full aspect-[4/3] overflow-hidden relative">
                    <img 
                      src={getImageUrl(article.image?.fileName, 'https://via.placeholder.com/600x400')}
                      alt={article.title}
                      className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-1000 ease-out"
                    />
                    <div className="absolute inset-0 bg-brand-primary/10 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
                    
                    <div className="absolute top-4 left-4 flex gap-2">
                      <span className="text-[10px] font-bold uppercase tracking-widest text-brand-text bg-white/90 backdrop-blur-md px-3 py-1 rounded-full shadow-sm">
                        {article.category?.name || 'BLOG'}
                      </span>
                    </div>
                  </div>
                  
                  <div className="p-6 md:p-8 flex flex-col flex-grow bg-white/50 backdrop-blur-sm">
                    <div className="flex items-center gap-2 mb-4 text-[11px] font-bold uppercase tracking-widest text-brand-muted">
                      <svg className="w-4 h-4 text-brand-secondary" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" /></svg>
                      <span>{new Date(article.createdDate).toLocaleDateString('tr-TR')}</span>
                    </div>
                    
                    <h4 className="text-2xl font-display font-bold text-brand-text leading-tight group-hover:text-brand-primary transition-colors mb-4 line-clamp-3">
                      {article.title}
                    </h4>
                    
                    <p className="text-brand-muted text-sm line-clamp-2 mt-auto font-medium">
                      {article.content ? article.content.replace(/<[^>]+>/g, '').substring(0, 150) : ''}...
                    </p>
                    
                    <div className="mt-6 pt-4 border-t border-brand-border/50 flex justify-between items-center text-[11px] font-bold uppercase tracking-widest text-brand-primary">
                      <span className="flex items-center gap-2">
                         <div className="w-6 h-6 rounded-full bg-brand-text overflow-hidden flex items-center justify-center text-white text-[10px] relative">
                           {article.user?.image ? (
                             <img 
                               src={getImageUrl(article.user.image.fileName)}
                               alt={`${article.user.firstName} ${article.user.lastName}`}
                               className="w-full h-full object-cover"
                             />
                           ) : (
                             <>{article.user?.firstName?.charAt(0)}{article.user?.lastName?.charAt(0)}</>
                           )}
                         </div>
                         <span className="text-brand-text">{article.user?.firstName} {article.user?.lastName}</span>
                      </span>
                      <span className="group-hover:text-brand-secondary transition-colors">Devamını Oku &rarr;</span>
                    </div>
                  </div>
                </Link>
              </div>
            ))}
          </div>
        )}
      </div>
      
    </div>
  );
}
