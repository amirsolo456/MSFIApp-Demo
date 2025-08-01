using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

public class MediaHandler
{
    public async Task TakeOrPickPhotoAsync()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            // گرفتن عکس با استفاده از دوربین
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // ذخیره عکس در فضای ذخیره‌سازی داخلی
                var filePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
                Console.WriteLine($"عکس گرفته شده با موفقیت ذخیره شد: {filePath}");
            }
        }
        else
        {
            // انتخاب عکس از گالری
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                // ذخیره عکس در فضای ذخیره‌سازی داخلی
                var filePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
                Console.WriteLine($"عکس انتخاب شده از گالری با موفقیت ذخیره شد: {filePath}");
            }
        }
    }
}
