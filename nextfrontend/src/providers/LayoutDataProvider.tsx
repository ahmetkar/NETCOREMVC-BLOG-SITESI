'use client';
import { createContext, useContext, useState, useEffect } from 'react';
import { fetchGenelVeriler } from '@/services/visitor/generalService';
import { LayoutDataDto } from '@/types/dto';

const LayoutDataContext = createContext<LayoutDataDto | null>(null);

export const LayoutDataProvider = ({ children }: { children: React.ReactNode }) => {
  const [data, setData] = useState<LayoutDataDto | null>(null);
  
  useEffect(() => {
    fetchGenelVeriler()
      .then(res => setData(res))
      .catch(err => console.error("Layout data fetch error:", err));
  }, []);

  return (
    <LayoutDataContext.Provider value={data}>
      {children}
    </LayoutDataContext.Provider>
  );
};

export const useLayoutData = () => useContext(LayoutDataContext);
