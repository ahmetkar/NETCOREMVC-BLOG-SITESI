import type { Metadata } from 'next';
import Header from '@/components/public/Header';
import Footer from '@/components/public/Footer';
import { LayoutDataProvider } from '@/providers/LayoutDataProvider';

export const metadata: Metadata = {
  title: 'Blog',
  description: 'Premium Editorial Platform',
};

export default function PublicLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <LayoutDataProvider>
      <div className="min-h-screen flex flex-col bg-brand-bg text-brand-text font-sans selection:bg-brand-primary selection:text-white relative">
        {/* Global Aurora Background */}
        <div className="fixed inset-0 w-full h-full pointer-events-none z-[-1] aurora-bg opacity-70"></div>
        
        <Header />
        <main className="flex-grow pt-[100px]">
          {children}
        </main>
        <Footer />
      </div>
    </LayoutDataProvider>
  );
}
