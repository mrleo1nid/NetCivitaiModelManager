using CivitaiApi.CivitaiDataContracts;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class ModelsExtensions
    {
        public static ImageUrlAndPath? CreateImagePath(this LocalModel localModel)
        {
            if (localModel.ExternalModel == null) return new ImageUrlAndPath() {Error = "Внешняя модель не найдена." };
            var image = localModel.ExternalModel.Images.FirstOrDefault();
            if (image != null)
            {
                var urlandpath = new ImageUrlAndPath() { Url = image.Url };
                if(localModel.LocalFile==null) return new ImageUrlAndPath() { Error = "Локальный файл не найден." };
                var directory = Path.GetDirectoryName(localModel.LocalFile.FullName);
                var name = Path.GetFileNameWithoutExtension(localModel.LocalFile.FullName);
                urlandpath.Path = Path.Combine(directory, name) + ".preview.png";
                return urlandpath;
            }
            else return new ImageUrlAndPath() { Error = "Изображение не найдено." };
        }

    }
}
