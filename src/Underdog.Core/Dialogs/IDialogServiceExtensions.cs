using System;
using System.Threading.Tasks;

namespace Underdog.Core.Dialogs;

/// <summary>
/// Provides compatibility Extensions for the <see cref="IDialogService"/>
/// </summary>
public static class IDialogServiceExtensions
{
    /// <summary>
    /// Shows the dialog with the given name and passes parameters to the dialog
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="parameters">The <see cref="IDialogParameters"/> to pass to the dialog</param>
    /// <param name="windowName">The name of the window</param>
    public static void ShowDialog(this IDialogService dialogService, string name, IDialogParameters parameters, string? windowName = null) =>
        dialogService.ShowDialog(name, parameters, DialogCallback.Empty, windowName);

    /// <summary>
    /// Shows the dialog with the given name and passes an empty set of DialogParameters
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="windowName">The name of the window</param>
    public static void ShowDialog(this IDialogService dialogService, string name, string? windowName = null) =>
        dialogService.ShowDialog(name, new DialogParameters(), windowName);

    /// <summary>
    /// Shows a dialog with a given name which needs no parameters but has a <see cref="DialogCallback"/>
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="callback">A specified <see cref="DialogCallback"/>.</param>
    /// <param name="windowName">The name of the window</param>
    public static void ShowDialog(this IDialogService dialogService, string name, DialogCallback callback, string? windowName = null) =>
        dialogService.ShowDialog(name, new DialogParameters(), callback, windowName);

    /// <summary>
    /// Shows a Dialog with a given name and an <see cref="Action"/> for a callback
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="callback"></param>
    /// <param name="windowName">The name of the window</param>
    /// <remarks>This is for backwards compatibility. Use DialogCallback instead.</remarks>
    public static void ShowDialog(this IDialogService dialogService, string name, Action callback, string? windowName = null) =>
        dialogService.ShowDialog(name, null, callback, windowName);

    /// <summary>
    /// Shows a Dialog with a given name and an <see cref="Action{IDialogResult}"/> for a callback
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="callback"></param>
    /// <param name="windowName">The name of the window</param>
    /// <remarks>This is for backwards compatibility. Use DialogCallback instead.</remarks>
    public static void ShowDialog(this IDialogService dialogService, string name, Action<IDialogResult> callback, string? windowName = null) =>
        dialogService.ShowDialog(name, null, callback, windowName);

    /// <summary>
    /// Shows a Dialog with a given name and an <see cref="Action"/> for a callback.
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="parameters">The <see cref="IDialogParameters"/> to pass to the dialog</param>
    /// <param name="callback"></param>
    /// <param name="windowName">The name of the window</param>
    /// <remarks>This is for backwards compatibility. Use DialogCallback instead.</remarks>
    public static void ShowDialog(this IDialogService dialogService, string name, IDialogParameters parameters, Action callback, string? windowName = null) =>
        dialogService.ShowDialog(name, parameters, new DialogCallback().OnClose(callback), windowName);

    /// <summary>
    /// Shows a Dialog with a given name and an <see cref="Action{IDialogResult}"/> for a callback
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="parameters">The <see cref="IDialogParameters"/> to pass to the dialog</param>
    /// <param name="callback"></param>
    /// <param name="windowName">The name of the window</param>
    /// <remarks>This is for backwards compatibility. Use DialogCallback instead.</remarks>
    public static void ShowDialog(this IDialogService dialogService, string name, IDialogParameters parameters, Action<IDialogResult> callback, string? windowName = null) =>
        dialogService.ShowDialog(name, parameters, new DialogCallback().OnClose(callback), windowName);

    /// <summary>
    /// Asynchronously shows the Dialog and returns the <see cref="IDialogResult"/>.
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="windowName">The name of the window</param>
    /// <returns>An <see cref="IDialogResult"/> on the close of the dialog.</returns>
    public static Task<IDialogResult> ShowDialogAsync(this IDialogService dialogService, string name, string? windowName = null) =>
        dialogService.ShowDialogAsync(name, new DialogParameters(), windowName);

    /// <summary>
    /// Asynchronously shows the Dialog and returns the <see cref="IDialogResult"/>, with given <see cref="IDialogParameters"/>.
    /// </summary>
    /// <param name="dialogService">The <see cref="IDialogService"/>.</param>
    /// <param name="name">The name of the dialog</param>
    /// <param name="parameters">The <see cref="IDialogParameters"/> to pass to the dialog</param>
    /// <param name="windowName">The name of the window</param>
    /// <returns>An <see cref="IDialogResult"/> on the close of the dialog.</returns>
    public static Task<IDialogResult> ShowDialogAsync(this IDialogService dialogService, string name, IDialogParameters parameters, string? windowName = null)
    {
        var tcs = new TaskCompletionSource<IDialogResult>();
        dialogService.ShowDialog(name, parameters, new DialogCallback().OnClose(result =>
        {
            if (result.Exception is DialogException de && de.Message == DialogException.CanCloseIsFalse)
                return;
            else if (result.Exception is not null)
                tcs.TrySetException(result.Exception);
            else
                tcs.TrySetResult(result);
        }), windowName);
        return tcs.Task;
    }
}
