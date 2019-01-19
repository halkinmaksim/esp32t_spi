#   Halkin Maksim

import time
import machine
from machine import Pin as GPIO
import ujson


class MAX6675:
    def __init__(self):
        self.spi = machine.SPI(spihost=1, sck=18, miso=19, mosi=21, duplex=False)
        self.dict_cs_pins = (5,15,2,4,22)
        self.dict_cs_obj = []
        for num in self.dict_cs_pins:
            self.dict_cs_obj.append(GPIO(num,GPIO.OUT))

        for obj in self.dict_cs_obj:
            obj.value(1)


    def getTemperature(self,num_sens):
        self.dict_cs_obj[num_sens].value(0)
        a = self.spi.read(2)
        #print((((a[0] << 8) | (a[1])) >> 3) * 0.25)
        self.dict_cs_obj[num_sens].value(1)
        return (((a[0] << 8) | (a[1])) >> 3) * 0.25
        pass

    def getAllTemperature(self):
        data_device = {}
        i=0
        while i< len(self.dict_cs_obj):
            data_device["t"+str(i)]=self.getTemperature(i)
            i=i+1
        data_device = ujson.dumps(data_device)
        print(data_device)
