﻿using System.Runtime.CompilerServices;

namespace Alpaca.Markets.Extensions;

/// <summary>
/// Helper task-like type for providing cancellation support for `params`-style extension methods.
/// </summary>
public readonly struct AlpacaValueTask : IEquatable<AlpacaValueTask>
{
    private readonly Func<CancellationToken, ValueTask> _capturedFunction;

    private readonly CancellationToken _cancellationToken;

    /// <summary>
    /// Creates new instance of the <see cref="AlpacaValueTask"/> structure.
    /// </summary>
    public AlpacaValueTask()
    {
        _capturedFunction = (CancellationToken _) => new ValueTask();
        _cancellationToken = CancellationToken.None;
    }

    internal AlpacaValueTask(
        Func<CancellationToken, ValueTask> capturedFunction,
        CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        _capturedFunction = capturedFunction;
    }

    /// <summary>
    /// Gets a new instance of the <see cref="AlpacaValueTask"/> with cancellation token.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for awaited operation.</param>
    /// <returns>New wrapper over original task with the configured cancellation token.</returns>
    [UsedImplicitly]
    public AlpacaValueTask WithCancellation(
        CancellationToken cancellationToken) =>
        new(_capturedFunction, cancellationToken);

    /// <summary>
    /// Gets the awaiter object for the async state machine generated by compiler.
    /// </summary>
    [UsedImplicitly]
    public ValueTaskAwaiter GetAwaiter() =>
#pragma warning disable CA2012 // Use ValueTasks correctly
            _capturedFunction.Invoke(_cancellationToken).GetAwaiter();
#pragma warning restore CA2012 // Use ValueTasks correctly

    /// <inheritdoc />
    public Boolean Equals(AlpacaValueTask other) =>
        _capturedFunction.Equals(other._capturedFunction) &&
        // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
        _cancellationToken.Equals(other._cancellationToken);

    /// <inheritdoc />
    public override Boolean Equals(
        Object? obj) =>
        obj is AlpacaValueTask other &&
        Equals(other);

    /// <inheritdoc />
    public override Int32 GetHashCode() =>
        _capturedFunction.GetHashCode();

    /// <summary>
    /// Returns <c>true</c> if <paramref name="lhs"/> are equal to <paramref name="rhs"/>.
    /// </summary>
    /// <param name="lhs">Left hand side object.</param>
    /// <param name="rhs">Right hand side object.</param>
    /// <returns>True if both objects are equal.</returns>
    public static Boolean operator ==(
        AlpacaValueTask lhs,
        AlpacaValueTask rhs) =>
        lhs.Equals(rhs);

    /// <summary>
    /// Returns <c>true</c> if <paramref name="lhs"/> are not equal to <paramref name="rhs"/>.
    /// </summary>
    /// <param name="lhs">Left hand side object.</param>
    /// <param name="rhs">Right hand side object.</param>
    /// <returns>True if both objects are not equal.</returns>
    public static Boolean operator !=(
        AlpacaValueTask lhs,
        AlpacaValueTask rhs) =>
        !lhs.Equals(rhs);
}
