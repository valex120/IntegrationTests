namespace Marketplace.Tests.TestCaseEntities
{
    /// <summary>
    ///     Базовый тест-кейс
    /// </summary>
    public abstract class TestCaseBase
    {
        /// <summary>
        ///     Идентификатор тест кейса
        /// </summary>
        public string TestId { get; set; } = default!;

        /// <summary>
        ///     Описание тест кейса
        /// </summary>
        public string Description { get; set; } = default!;
    }
}