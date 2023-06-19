namespace TestEnvironment
{
    /// <summary>
    /// Fixtures for <see cref="PageModel" />.
    /// </summary>
    public class Web
    {
        #region Methods

        /// <summary>
        /// Creates a <see cref="PageModel" /> as a fixture.
        /// </summary>
        /// <typeparam name="TPageModel">
        /// Type of the <see cref="PageModel" />.
        /// </typeparam>
        /// <param name="args">
        /// Arguments passed into the constructor.
        /// </param>
        /// <returns>
        /// Explicit <see cref="PageModel" />.
        /// </returns>
        public static TPageModel CreatePageModelFixture<TPageModel>(object?[]? args)
            where TPageModel : PageModel
        {
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();

            var actionContext = new ActionContext(httpContext,
                new RouteData(),
                new PageActionDescriptor(),
                modelState);

            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = Activator.CreateInstance(typeof(TPageModel), args) as TPageModel;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            pageModel.PageContext = pageContext;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            pageModel.TempData = tempData;
            pageModel.Url = new UrlHelper(actionContext);
            return pageModel;
        }

        /// <summary>
        /// Setup of a mocked context for <see cref="IAsyncActionFilter" /> s.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ActionExecutingContext" /> mocked by this setup.
        /// </param>
        /// <param name="actionExecutedContext">
        /// The <see cref="ActionExecutedContext" /> mocked by this setup.
        /// </param>
        /// <param name="executionDelegate">
        /// The <see cref="ActionExecutionDelegate" /> mocked by this setup is the next method in
        /// the pipeline.
        /// </param>
        /// <param name="httpMethod">
        /// The HTTPMethod used in the filter. I use this method to write validation filters for a
        /// API policy. The default ist <c>null</c>.
        /// </param>
        /// <param name="actionArguments">
        /// ActionArguments of the context. Default is <c>null</c>.
        /// </param>
        public static void SetupIAsyncActionFilterContext(out ActionExecutingContext context,
            out ActionExecutedContext actionExecutedContext,
            out ActionExecutionDelegate executionDelegate,
            string? httpMethod = null,
            IDictionary<string, object?>? actionArguments = null)
        {
            actionArguments ??= Mock.Of<IDictionary<string, object?>>();

            var httpContext = Mock.Of<HttpContext>(hc =>
                hc.Request == Mock.Of<HttpRequest>(hr =>
                    hr.Method == httpMethod));

            var actionContext = Mock.Of<ActionContext>(ac =>
                ac.HttpContext == httpContext
                && ac.RouteData == Mock.Of<RouteData>()
                && ac.ActionDescriptor == Mock.Of<ActionDescriptor>());

            var filters = Mock.Of<IList<IFilterMetadata>>();

            var controller = Mock.Of<object>();

            context = new ActionExecutingContext(actionContext,
                filters,
                actionArguments,
                controller);

            var executedContext = new ActionExecutedContext(actionContext,
                filters,
                controller);

            actionExecutedContext = executedContext;

            executionDelegate = new ActionExecutionDelegate(() =>
            Task.FromResult(executedContext));
        }

        #endregion Methods
    }
}