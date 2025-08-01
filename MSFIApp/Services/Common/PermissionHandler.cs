using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using System;
using System.Threading.Tasks;

public class PermissionHandler
{
    public async Task RequestPermissionsAsync()
    {
        // بررسی و درخواست دسترسی به دوربین
        var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (cameraStatus != PermissionStatus.Granted)
        {
            cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
        }

        // بررسی و درخواست دسترسی به گالری
        var photosStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();
        if (photosStatus != PermissionStatus.Granted)
        {
            photosStatus = await Permissions.RequestAsync<Permissions.Photos>();
        }

        // بررسی و درخواست دسترسی به فضای ذخیره‌سازی
        var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        if (storageStatus != PermissionStatus.Granted)
        {
            storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
        }

        // بررسی و درخواست دسترسی به اینترنت
        var internetStatus = await Permissions.CheckStatusAsync<Permissions.NetworkState>();
        if (internetStatus != PermissionStatus.Granted)
        {
            internetStatus = await Permissions.RequestAsync<Permissions.NetworkState>();
        }

        // بررسی وضعیت دسترسی‌ها
        if (cameraStatus == PermissionStatus.Granted &&
            photosStatus == PermissionStatus.Granted &&
            storageStatus == PermissionStatus.Granted &&
            internetStatus == PermissionStatus.Granted)
        {
            Console.WriteLine("تمام دسترسی‌ها با موفقیت اعطا شدند.");
        }
        else
        {
            Console.WriteLine("برخی از دسترسی‌ها رد شدند.");
        }
    }
}
