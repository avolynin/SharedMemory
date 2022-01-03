using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace MemoryTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var imageBytes = ConvertImageToBytes(@"D:\Users\Camputer\source\repos\MemoryTest\image.jpg");

			//Для передачи текста:
			//Console.Write("Enter any message: ");
			//var message = Console.ReadLine().ToCharArray();
			//var size = message.Length * sizeof(char) + sizeof(int);

			var sharedMemory = MemoryMappedFile.CreateNew("Name of map", imageBytes.Length);
			using(var writer = sharedMemory.CreateViewAccessor(0, imageBytes.Length))
			{
				writer.Write(0, imageBytes.Length);
				writer.WriteArray<byte>(0, imageBytes, 0, imageBytes.Length);
			}

			Console.WriteLine("Сообщение/изображение записано в разделяемую память");
			Console.WriteLine("Для выхода из программы нажмите любую клавишу");
			Console.ReadLine();
		}

		static byte[] ConvertImageToBytes(string filePath)
		{
			byte[] imageData = null;
			FileInfo fileInfo = new FileInfo(filePath);
			long imageFileLength = fileInfo.Length;
			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);
			imageData = br.ReadBytes((int)imageFileLength);
			return imageData;
		}
	}
}
