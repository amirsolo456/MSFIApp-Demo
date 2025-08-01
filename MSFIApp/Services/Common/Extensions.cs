namespace MSFIApp.Services.Common
{
    public static class PageExtensions
    {
        public static Page GetCurrentPage(this Page page)
        {
            if (page == null)
                return null;

            // اگر صفحه از نوع Shell باشد
            if (page is Shell shell)
            {
                // صفحه جاری Shell
                return shell.CurrentPage.GetCurrentPage();
            }

            // اگر صفحه از نوع NavigationPage باشد
            if (page is NavigationPage navPage)
            {
                var currentNavPage = navPage.CurrentPage;
                return currentNavPage.GetCurrentPage();
            }

            // اگر صفحه از نوع TabbedPage باشد
            if (page is TabbedPage tabbedPage)
            {
                var currentTab = tabbedPage.CurrentPage;
                return currentTab.GetCurrentPage();
            }

            // اگر صفحه از نوع Modal است (مثلاً اگر صفحه Modal باز باشد)
            if (page.Navigation.ModalStack.Count > 0)
            {
                var modal = page.Navigation.ModalStack.LastOrDefault();
                if (modal != null)
                    return modal.GetCurrentPage();
            }

            // در نهایت اگر هیچکدام نبود، همان صفحه را برگردان
            return page;
        }
    }
}
