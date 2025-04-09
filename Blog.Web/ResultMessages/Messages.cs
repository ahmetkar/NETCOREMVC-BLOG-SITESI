namespace Blog.Web.ResultMessages
{

    public static class Messages
    {
        public static string Add(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla eklendi.";
        }

        public static string Update(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla güncellendi.";
        }

        public static string Delete(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla silindi.";
        }

        public static string UndoDelete(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla geri alındı";
        }

    }

    public static class CategoryMessages
    {
        public static string Add(string categoryName)
        {
            return $"{categoryName} adlı kategori başarıyla eklendi.";
        }

        public static string Update(string categoryName)
        {
            return $"{categoryName} adlı kategori başarıyla güncellendi.";
        }

        public static string Delete(string categoryName)
        {
            return $"{categoryName} adlı kategori başarıyla silindi.";
        }

        public static string UndoDelete(string categoryName)
        {
            return $"{categoryName} adlı kategori başarıyla geri alındı.";
        }
    }

    public static class TagMessages
    {
        public static string Add(string tagName)
        {
            return $"{tagName} adlı etiket başarıyla eklendi.";
        }

        public static string Update(string tagName)
        {
            return $"{tagName} adlı etiket başarıyla güncellendi.";
        }

        public static string Delete(string tagName)
        {
            return $"{tagName} adlı etiket başarıyla silindi.";
        }

        public static string UndoDelete(string tagName)
        {
            return $"{tagName} adlı etiket başarıyla geri alındı.";
        }
    }

    public static class UserMessages
    {
        public static string Add(string userName)
        {
            return $"{userName} adlı kullanıcı başarıyla eklendi.";
        }

        public static string Update(string userName)
        {
            return $"{userName} adlı kullanıcı başarıyla güncellendi.";
        }

        public static string Delete(string userName)
        {
            return $"{userName} adlı kullanıcı başarıyla silindi.";
        }
    }


}
