'use client';
import Link from 'next/link';
import { getImageUrl } from '@/lib/image-url';

export default function CategoryDetailClient({
  category,
  articles
}: {
  category: any;
  articles: any[];
}) {
  if (!category) return null;

  return (
    <div className="w-full relative min-h-screen">
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[10%] left-[20%] w-[30%] h-[30%] rounded-full bg-brand-primary/10 blur-[120px] pointer-events-none -z-10"></div>
      
      {/* Header */}
      <div className="py-20 text-center border-b border-brand-border/30 mb-16 relative">
         <span className="text-[11px] font-bold uppercase tracking-widest text-brand-primary mb-4 block">Kategori</span>
         <h1 className="text-5xl sm:text-6xl lg:text-7xl font-display font-black text-brand-text mb-6">
           {category.name}
         </h1>
      </div>

      {/* Articles Grid */}
      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 pb-24">
        {articles.length === 0 ? (
          <div className="text-center py-20">
            <span className="text-brand-muted font-medium text-lg">Bu kategoride henüz yazı bulunmuyor.</span>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
             {articles.map((article: any) => (
               <div key={article.id} className="glass rounded-3xl overflow-hidden shadow-sm hover:shadow-xl hover:shadow-brand-primary/10 transition-all duration-300 transform hover:-translate-y-1">
                 <Link href={`/blog/${article.slug || article.id}`} className="group flex flex-col h-full">
                   {/* Image */}
                   <div className="w-full aspect-[4/3] bg-brand-border overflow-hidden relative">
                     <img 
                       src={getImageUrl(article.image?.fileName, 'https://via.placeholder.com/800x600')}
                       alt={article.title}
                       className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700 ease-out"
                     />
                     <div className="absolute top-4 left-4">
                        <span className="text-[10px] font-bold uppercase tracking-widest text-white bg-black/50 backdrop-blur-md px-3 py-1 rounded-full">
                           {new Date(article.createdDate).toLocaleDateString('tr-TR')}
                        </span>
                     </div>
                   </div>
                   
                   {/* Content */}
                   <div className="p-6 md:p-8 flex flex-col flex-grow">
                     <h4 className="text-2xl font-display font-bold text-brand-text leading-tight group-hover:text-brand-primary transition-colors mb-4 line-clamp-3">
                       {article.title}
                     </h4>
                     <p className="text-brand-muted text-sm line-clamp-2 mb-6 font-medium flex-grow">
                       {article.content ? article.content.replace(/<[^>]+>/g, '') : ''}
                     </p>
                     <div className="mt-auto flex items-center justify-between text-[11px] font-bold uppercase tracking-widest text-brand-text border-t border-brand-border/50 pt-4">
                        <span className="flex items-center gap-2">
                           <div className="w-5 h-5 rounded-full bg-gradient-to-tr from-brand-primary to-brand-secondary"></div>
                           {article.user?.firstName || 'Yazar'}
                        </span>
                        <span className="text-brand-primary group-hover:text-brand-secondary transition-colors">Yazıyı Oku &rarr;</span>
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
