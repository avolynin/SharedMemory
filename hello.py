from multiprocessing import shared_memory
import io
from PIL import Image

sharedMemory = shared_memory.SharedMemory("Name of map")

imageBinaryBytes = bytes(sharedMemory.buf)
imageStream = io.BytesIO(imageBinaryBytes)
imageFilePNG = Image.open(imageStream).convert("RGBA")
imageFileJPG = Image.open(imageStream).convert("RGB")
imageFilePNG.save("savedImage.png")
imageFileJPG.save("savedImage.jpg")

sharedMemory.close()
imageStream.close()