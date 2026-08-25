'use client';
import { useLayoutData } from '@/providers/LayoutDataProvider';
import Link from 'next/link';

export default function Kategoriler() {
  const layoutData = useLayoutData();
  const categories = layoutData?.categories || [];

  return (
    <div className="w-full relative py-12 md:py-24">
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[10%] left-[5%] w-[40%] h-[40%] rounded-full bg-brand-primary/10 blur-[120px] pointer-events-none -z-10"></div>
      <div className="absolute top-[30%] right-[10%] w-[30%] h-[30%] rounded-full bg-brand-secondary/10 blur-[100px] pointer-events-none -z-10"></div>

      {/* Header */}
      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 mb-16 text-center">
         <h1 className="text-5xl sm:text-6xl lg:text-7xl font-display font-black text-brand-text mb-6">
           Keşfet
         </h1>
         <p className="text-xl text-brand-muted font-medium max-w-2xl mx-auto">
           Tüm konuları, ilgi alanlarını ve makale türlerini keşfedin.
         </p>
      </div>

      {/* Category Grid */}
      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12">
        {categories.length === 0 ? (
          <div className="text-center py-20">
            <span className="text-[11px] font-bold uppercase tracking-widest text-brand-muted">Kategori bulunamadı.</span>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {categories.map((category: any, index: number) => (
              <Link 
                key={category.id} 
                href={`/kategoriler/${category.slug || category.id}`} 
                className="group block p-8 rounded-3xl glass hover:shadow-2xl hover:shadow-brand-primary/10 transition-all duration-300 transform hover:-translate-y-2 border border-white/40"
              >
                <div className="flex flex-col h-full justify-between gap-8">
                  <div>
                    <h2 className="text-3xl font-display font-bold text-brand-text group-hover:text-brand-primary transition-colors mb-2">
                      {category.name}
                    </h2>
                    <p className="text-brand-muted text-sm line-clamp-2">
                      Bu kategorideki tüm güncel yazıları ve içerikleri görüntüleyin.
                    </p>
                  </div>
                  <div className="flex items-center justify-between mt-4">
                    <span className="text-[11px] font-bold uppercase tracking-widest text-brand-muted group-hover:text-brand-primary transition-colors">Makaleleri Gör</span>
                    <div className="w-10 h-10 rounded-full bg-brand-bg flex items-center justify-center text-brand-text group-hover:bg-brand-primary group-hover:text-white transition-colors shadow-sm">
                      <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M14 5l7 7m0 0l-7 7m7-7H3"></path></svg>
                    </div>
                  </div>
                </div>
              </Link>
            ))}
          </div>
        )}
      </div>

    </div>
  );
}
