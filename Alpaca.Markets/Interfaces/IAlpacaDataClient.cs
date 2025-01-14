﻿namespace Alpaca.Markets;

/// <summary>
/// Provides unified type-safe access for Alpaca Data API via HTTP/REST.
/// </summary>
[CLSCompliant(false)]
public interface IAlpacaDataClient :
    IHistoricalQuotesClient<HistoricalQuotesRequest>,
    IHistoricalTradesClient<HistoricalTradesRequest>,
    IHistoricalBarsClient<HistoricalBarsRequest>,
    IDisposable
{
    /// <summary>
    /// Gets last trade for singe asset from Alpaca REST API endpoint.
    /// </summary>
    /// <param name="symbol">Asset name for data retrieval.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Read-only last trade information.</returns>
    [UsedImplicitly]
    Task<ITrade> GetLatestTradeAsync(
        String symbol,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets current quote for singe asset from Alpaca REST API endpoint.
    /// </summary>
    /// <param name="symbol">Asset name for data retrieval.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Read-only current quote information.</returns>
    [UsedImplicitly]
    Task<IQuote> GetLatestQuoteAsync(
        String symbol,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets current snapshot (latest trade/quote and minute/days bars) for singe asset from Alpaca REST API endpoint.
    /// </summary>
    /// <param name="symbol">Asset name for data retrieval.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Read-only current snapshot information.</returns>
    [UsedImplicitly]
    Task<ISnapshot> GetSnapshotAsync(
        String symbol,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets current snapshot (latest trade/quote and minute/days bars) for several assets from Alpaca REST API endpoint.
    /// </summary>
    /// <param name="symbols">List of asset names for data retrieval.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Read-only dictionary with the current snapshot information.</returns>
    [UsedImplicitly]
    Task<IReadOnlyDictionary<String, ISnapshot>> GetSnapshotsAsync(
        IEnumerable<String> symbols,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets dictionary with exchange code to the exchange name mappings.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns></returns>
    [UsedImplicitly]
    Task<IReadOnlyDictionary<String, String>> ListExchangesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets dictionary with trades conditions code to the condition description mappings.
    /// </summary>
    /// <param name="tape">SIP tape identifier for retrieving trade conditions.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns></returns>
    [UsedImplicitly]
    Task<IReadOnlyDictionary<String, String>> ListTradeConditionsAsync(
        Tape tape,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets dictionary with quotes conditions code to the condition description mappings.
    /// </summary>
    /// <param name="tape">SIP tape identifier for retrieving quote conditions.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns></returns>
    [UsedImplicitly]
    Task<IReadOnlyDictionary<String, String>> ListQuoteConditionsAsync(
        Tape tape,
        CancellationToken cancellationToken = default);
}
