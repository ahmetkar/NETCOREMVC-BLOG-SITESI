import { ConfigProvider } from 'antd';
import trTR from 'antd/locale/tr_TR';
import '@/app/globals.css';
import AntdRegistry from '@/lib/AntdRegistry';

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="tr">
      <body>
        <AntdRegistry>
          <ConfigProvider locale={trTR} theme={{ token: { colorPrimary: '#1677ff' } }}>
            {children}
          </ConfigProvider>
        </AntdRegistry>
      </body>
    </html>
  );
}
