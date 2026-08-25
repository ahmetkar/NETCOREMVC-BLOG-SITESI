'use client';
import { useState, useEffect, FormEvent } from 'react';
import { postMakaleYorum } from '@/services/visitor/generalService';
import Link from 'next/link';
import { getImageUrl } from '@/lib/image-url';
import DOMPurify from 'isomorphic-dompurify';
import { ArticleDto, CommentDto, ArticleDetailClientProps } from '@/types/dto';

export default function ArticleDetailClient({
  article,
  mayLikeArticles
}: ArticleDetailClientProps) {
  const [readingProgress, setReadingProgress] = useState(0);
  const [commentForm, setCommentForm] = useState({ name: '', email: '', text: '' });
  const [commentStatus, setCommentStatus] = useState({ type: '', message: '' });
  const [commentLoading, setCommentLoading] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      const totalScroll = document.documentElement.scrollTop;
      const windowHeight = document.documentElement.scrollHeight - document.documentElement.clientHeight;
      if (windowHeight > 0) {
        setReadingProgress(totalScroll / windowHeight);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const handleCommentSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setCommentLoading(true);
    setCommentStatus({ type: '', message: '' });

    // Anti-bot honeypot check
    const form = e.currentTarget;
    const honeypotInput = form.elements.namedItem('hp_comment_check') as HTMLInputElement | null;
    const honeypot = honeypotInput?.value;
    if (honeypot) {
      setCommentStatus({ type: 'success', message: 'Yorumunuz başarıyla gönderildi.' });
      setCommentForm({ name: '', email: '', text: '' });
      setCommentLoading(false);
      return;
    }

    const formData = new FormData();
    formData.append('id', article.id);
    formData.append('name', commentForm.name);
    formData.append('email', commentForm.email);
    formData.append('text', commentForm.text);

    try {
      const res = await postMakaleYorum(formData);
      if (res.success) {
        setCommentStatus({ type: 'success', message: res.message || 'Yorumunuz başarıyla gönderildi.' });
        setCommentForm({ name: '', email: '', text: '' });
      } else {
        setCommentStatus({ type: 'error', message: res.message || 'Bir hata oluştu.' });
      }
    } catch (err: unknown) {
      const errorObj = err as { response?: { data?: { message?: string } } };
      setCommentStatus({ type: 'error', message: errorObj.response?.data?.message || 'Bir hata oluştu.' });
    }
    setCommentLoading(false);
  };

  if (!article) return null;

  return (
    <div className="w-full relative bg-brand-surface selection:bg-brand-primary selection:text-white pb-32">
      
      {/* Reading Progress Bar */}
      <div className="fixed top-0 left-0 w-full h-1.5 bg-transparent z-[60]">
        <div className="h-full bg-gradient-to-r from-brand-primary to-brand-secondary rounded-r-full" style={{ width: `${readingProgress * 100}%` }}></div>
      </div>

      {/* Hero Section */}
      <div className="relative pt-24 pb-16 md:pt-32 md:pb-24 overflow-hidden">
        {/* Decorative Aurora Elements */}
        <div className="absolute top-0 left-1/2 -translate-x-1/2 w-full max-w-4xl h-[500px] rounded-full bg-brand-primary/10 blur-[150px] pointer-events-none -z-10"></div>
        
        <div className="max-w-[1000px] mx-auto px-4 sm:px-6 lg:px-12 text-center relative z-10">
          
          <div className="flex items-center justify-center gap-4 mb-8">
            <Link href={`/kategoriler/${article.category?.slug || article.category?.id}`} className="text-[11px] font-bold uppercase tracking-widest text-brand-primary bg-brand-primary/10 px-4 py-2 rounded-full hover:bg-brand-primary hover:text-white transition-colors">
              {article.category?.name || 'BLOG'}
            </Link>
            <span className="text-brand-muted">•</span>
            <span className="text-[11px] font-bold uppercase tracking-widest text-brand-muted">
               {new Date(article.createdDate).toLocaleDateString('tr-TR')}
            </span>
            {article.viewCount !== undefined && (
              <>
                <span className="text-brand-muted">•</span>
                <span className="text-[11px] font-bold uppercase tracking-widest text-brand-muted flex items-center gap-1">
                  <svg className="w-3 h-3" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" /><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" /></svg>
                  {article.viewCount} Okunma
                </span>
              </>
            )}
          </div>

          <h1 className="text-4xl sm:text-5xl md:text-6xl lg:text-7xl font-display font-black text-brand-text mb-12 leading-tight tracking-tight text-balance">
            {article.title}
          </h1>

          <div className="flex items-center justify-center gap-4">
             <div className="w-12 h-12 rounded-full bg-brand-text overflow-hidden flex items-center justify-center text-white font-display font-bold shadow-lg shadow-black/10 relative">
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
             <div className="text-left">
               <div className="text-sm font-bold text-brand-text">{article.user?.firstName} {article.user?.lastName}</div>
               <div className="text-[10px] uppercase tracking-widest text-brand-muted font-bold">
                 {article.user?.role || 'Yazar'}
               </div>
             </div>
          </div>
          
        </div>
      </div>

      {/* Main Image */}
      <div className="max-w-[1200px] mx-auto px-4 sm:px-6 mb-16 md:mb-24 relative z-20">
         <div className="w-full aspect-[16/9] md:aspect-[21/9] bg-brand-border rounded-3xl overflow-hidden shadow-2xl shadow-black/10 relative group">
           <img 
             src={getImageUrl(article.image?.fileName)}
             alt={article.title}
             className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-1000 ease-out"
           />
           <div className="absolute inset-0 bg-brand-text/5 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
         </div>
      </div>

      {/* Article Content Layout */}
      <div className="max-w-[1200px] mx-auto px-4 sm:px-6 flex flex-col lg:flex-row gap-16 relative">
        
        {/* Sticky Social Share (Desktop) */}
        <div className="hidden lg:block w-[100px] shrink-0">
          <div className="sticky top-32 flex flex-col items-center gap-6">
            <span className="text-[10px] font-bold uppercase tracking-widest text-brand-muted [writing-mode:vertical-lr] rotate-180 mb-4">Paylaş</span>
            <div className="w-px h-12 bg-brand-border/50"></div>
            <button aria-label="X (Twitter)'da Paylaş" className="w-10 h-10 rounded-full neumorphic flex items-center justify-center text-brand-text hover:text-[#1DA1F2]" onClick={() => window.open(`https://twitter.com/intent/tweet?text=${encodeURIComponent(article.title)}&url=${encodeURIComponent(window.location.href)}`, '_blank')}>
              <svg className="w-4 h-4 fill-current" viewBox="0 0 24 24"><path d="M23.953 4.57a10 10 0 01-2.825.775 4.958 4.958 0 002.163-2.723c-.951.555-2.005.959-3.127 1.184a4.92 4.92 0 00-8.384 4.482C7.69 8.095 4.067 6.13 1.64 3.162a4.822 4.822 0 00-.666 2.475c0 1.71.87 3.213 2.188 4.096a4.904 4.904 0 01-2.228-.616v.06a4.923 4.923 0 003.946 4.827 4.996 4.996 0 01-2.212.085 4.936 4.936 0 004.604 3.417 9.867 9.867 0 01-6.102 2.105c-.39 0-.779-.023-1.17-.067a13.995 13.995 0 007.557 2.209c9.053 0 13.998-7.496 13.998-13.985 0-.21 0-.42-.015-.63A9.935 9.935 0 0024 4.59z"/></svg>
            </button>
            <button aria-label="Facebook'ta Paylaş" className="w-10 h-10 rounded-full neumorphic flex items-center justify-center text-brand-text hover:text-[#4267B2]" onClick={() => window.open(`https://www.facebook.com/sharer/sharer.php?u=${encodeURIComponent(window.location.href)}`, '_blank')}>
              <svg className="w-4 h-4 fill-current" viewBox="0 0 24 24"><path d="M24 12.073c0-6.627-5.373-12-12-12s-12 5.373-12 12c0 5.99 4.388 10.954 10.125 11.854v-8.385H7.078v-3.47h3.047V9.43c0-3.007 1.792-4.669 4.533-4.669 1.312 0 2.686.235 2.686.235v2.953H15.83c-1.491 0-1.956.925-1.956 1.874v2.25h3.328l-.532 3.47h-2.796v8.385C19.612 23.027 24 18.062 24 12.073z"/></svg>
            </button>
          </div>
        </div>

        {/* Content Body */}
        <div className="flex-grow max-w-[800px] w-full">
          <div 
            className="prose prose-lg md:prose-xl max-w-none text-brand-text prose-headings:font-display prose-headings:font-bold prose-headings:tracking-tight prose-a:text-brand-primary hover:prose-a:text-brand-secondary prose-a:transition-colors prose-img:rounded-2xl prose-img:shadow-lg prose-p:leading-relaxed prose-p:text-brand-text/90"
            dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(article.content || '') }}
          />

          {/* AUTHOR SECTION */}
          {article.user && (
            <div className="mt-16 pt-10 border-t border-brand-border/50">
               <div className="glass rounded-3xl p-8 flex flex-col md:flex-row gap-8 items-center md:items-start relative overflow-hidden">
                 <div className="w-32 h-32 rounded-full overflow-hidden shrink-0 border-4 border-brand-primary/20 shadow-lg relative z-10 bg-white">
                   {article.user.image ? (
                     <img 
                       src={getImageUrl(article.user.image.fileName)}
                       alt={`${article.user.firstName} ${article.user.lastName}`}
                       className="w-full h-full object-cover"
                     />
                   ) : (
                     <div className="w-full h-full bg-brand-primary/10 flex items-center justify-center font-display font-bold text-brand-primary text-4xl">
                       {article.user.firstName?.charAt(0)}{article.user.lastName?.charAt(0)}
                     </div>
                   )}
                 </div>
                 <div className="text-center md:text-left relative z-10">
                   <h3 className="text-2xl font-display font-bold text-brand-text mb-2">
                     {article.user.firstName} {article.user.lastName}
                   </h3>
                   <div className="text-sm font-bold uppercase tracking-widest text-brand-primary mb-4">
                     {article.user.role || 'Yazar'}
                   </div>
                   {article.user.biography ? (
                     <p className="text-brand-muted leading-relaxed font-medium">
                       {article.user.biography}
                     </p>
                   ) : (
                     <p className="text-brand-muted leading-relaxed font-medium italic opacity-70">
                       Yazar henüz bir biyografi eklememiş.
                     </p>
                   )}
                 </div>
               </div>
            </div>
          )}

          {/* MAY LIKE ARTICLES SECTION */}
          {mayLikeArticles && mayLikeArticles.length > 0 && (
            <div className="mt-20 pt-10 border-t border-brand-border/50">
              <h3 className="text-2xl font-display font-bold text-brand-text mb-8">Benzer İçerikler</h3>
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
                 {mayLikeArticles.map((simArticle: ArticleDto) => (
                   <Link key={simArticle.id} href={`/blog/${simArticle.slug || simArticle.id}`} className="group glass rounded-2xl p-4 flex gap-4 items-center hover:shadow-lg transition-shadow">
                     <div className="w-24 h-24 rounded-xl overflow-hidden bg-brand-border shrink-0">
                       <img 
                         src={getImageUrl(simArticle.image?.fileName)}
                         alt={simArticle.title}
                         className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500"
                       />
                     </div>
                     <div>
                       <h4 className="text-base font-bold text-brand-text leading-tight group-hover:text-brand-primary transition-colors line-clamp-2 mb-2">
                         {simArticle.title}
                       </h4>
                       <span className="text-[10px] font-bold uppercase tracking-widest text-brand-muted">
                         {new Date(simArticle.createdDate).toLocaleDateString('tr-TR')}
                       </span>
                     </div>
                   </Link>
                 ))}
              </div>
            </div>
          )}

          {/* COMMENTS SECTION */}
          <div className="mt-20 pt-10 border-t border-brand-border/50">
             <h3 className="text-3xl font-display font-bold text-brand-text mb-10">Yorumlar ({article.comments?.filter((c: CommentDto) => !c.isDeleted && c.isAprroved)?.length || 0})</h3>
             
             {/* Comments List */}
             <div className="space-y-8 mb-16">
                {article.comments?.filter((c: CommentDto) => !c.isDeleted && c.isAprroved).map((comment: CommentDto) => (
                  <div key={comment.id} className="glass rounded-3xl p-6 md:p-8">
                     <div className="flex items-center gap-4 mb-4">
                        <div className="w-12 h-12 rounded-full bg-brand-primary/10 flex items-center justify-center font-display font-bold text-brand-primary text-xl">
                          {comment.name?.charAt(0).toUpperCase()}
                        </div>
                        <div>
                          <div className="font-bold text-brand-text text-lg">{comment.name}</div>
                          <div className="text-[11px] font-bold uppercase tracking-widest text-brand-muted">
                            {comment.createdDate ? new Date(comment.createdDate).toLocaleDateString('tr-TR') : ''}
                          </div>
                        </div>
                     </div>
                     <p className="text-brand-muted font-medium leading-relaxed pl-16">
                       {comment.text || comment.commentText}
                     </p>
                  </div>
                ))}
             </div>

             {/* Add Comment Form */}
             <div className="neumorphic rounded-[2.5rem] p-8 md:p-12">
               <h4 className="text-2xl font-display font-bold text-brand-text mb-2">Yorum Yap</h4>
               <p className="text-brand-muted font-medium mb-8">Düşüncelerinizi bizimle paylaşın. E-posta adresiniz yayımlanmayacaktır.</p>
               
               <form onSubmit={handleCommentSubmit} className="space-y-6">
                  {/* Anti-spam honeypot */}
                  <input 
                    type="text" 
                    name="hp_comment_check" 
                    tabIndex={-1} 
                    autoComplete="off" 
                    className="hidden absolute w-0 h-0 opacity-0 pointer-events-none" 
                    aria-hidden="true" 
                  />
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">Adınız Soyadınız</label>
                      <input 
                        type="text" 
                        required
                        value={commentForm.name}
                        onChange={e => setCommentForm({...commentForm, name: e.target.value})}
                        className="w-full bg-white/50 border border-brand-border/50 rounded-full px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium text-brand-text shadow-inner"
                      />
                    </div>
                    <div className="space-y-2">
                      <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">E-posta Adresiniz</label>
                      <input 
                        type="email" 
                        required
                        value={commentForm.email}
                        onChange={e => setCommentForm({...commentForm, email: e.target.value})}
                        className="w-full bg-white/50 border border-brand-border/50 rounded-full px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium text-brand-text shadow-inner"
                      />
                    </div>
                  </div>
                  <div className="space-y-2">
                    <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">Yorumunuz</label>
                    <textarea 
                      required
                      rows={5}
                      value={commentForm.text}
                      onChange={e => setCommentForm({...commentForm, text: e.target.value})}
                      className="w-full bg-white/50 border border-brand-border/50 rounded-3xl px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium text-brand-text shadow-inner resize-none"
                    ></textarea>
                  </div>
                  
                  {commentStatus.message && (
                    <div className={`p-4 rounded-2xl text-sm font-bold ${commentStatus.type === 'success' ? 'bg-green-50 text-green-700' : 'bg-red-50 text-red-700'}`}>
                      {commentStatus.message}
                    </div>
                  )}

                  <button 
                    type="submit" 
                    disabled={commentLoading}
                    className="px-8 py-4 bg-brand-primary text-white text-[11px] font-bold uppercase tracking-widest rounded-full hover:bg-brand-primary/90 transition-all shadow-md shadow-brand-primary/20 disabled:opacity-50"
                  >
                    {commentLoading ? 'Gönderiliyor...' : 'Yorumu Gönder'}
                  </button>
               </form>
             </div>
          </div>
        </div>

        {/* Empty right column for balance (or TOC) */}
        <div className="hidden lg:block w-[100px] shrink-0"></div>

      </div>
    </div>
  );
}
