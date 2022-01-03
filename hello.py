from multiprocessing import shared_memory
import io, os
from PIL import Image

sharedMemory = shared_memory.SharedMemory("Name of map")

imageBinaryBytes = bytes(sharedMemory.buf)
imageStream = io.BytesIO(imageBinaryBytes)
imageFilePNG = Image.open(imageStream).convert("RGBA")
imageFileJPG = Image.open(imageStream).convert("RGB")

path = os.path.dirname(__file__)
imageFilePNG.save(path+"\savedImage.png")
imageFileJPG.save(path+"\savedImage.jpg")

sharedMemory.close()
imageStream.close()