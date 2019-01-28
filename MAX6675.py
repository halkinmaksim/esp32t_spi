#   Halkin Maksim

import time
import machine
from machine import Pin as GPIO
import ujson
import _thread



class MAX6675:
    def __init__(self):
        self.spi = machine.SPI(spihost=1, sck=18, miso=19, mosi=21, duplex=False)
        self.dict_cs_pins = (5,15,2,4,22)
        self.dict_cs_obj = []
        self.arr_data = [[] for _ in range(5)]
        self.arr_data_cnt = [0,0,0,0,0]
        self.arr_data_first = True

        self.data_temperature = {}
        for i in range(5):
            for j in range(16):
                self.arr_data[i].append(0)
        
        
        for num in self.dict_cs_pins:
            self.dict_cs_obj.append(GPIO(num,GPIO.OUT))

        for obj in self.dict_cs_obj:
            obj.value(1)

        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()
        self.getAllTemperature()


    def getSrednValue(self,num_sens):
        pass
    def getTemperature(self,num_sens):
        self.dict_cs_obj[num_sens].value(0)
        a = self.spi.read(2)
        #print((((a[0] << 8) | (a[1])) >> 3) * 0.25)
        self.dict_cs_obj[num_sens].value(1)
        self.arr_data[num_sens][self.arr_data_cnt[num_sens]]=(((a[0] << 8) | (a[1])) >> 3)
        self.arr_data_cnt[num_sens] = self.arr_data_cnt[num_sens]+1
        if(self.arr_data_cnt[num_sens]>15):
            self.arr_data_cnt[num_sens] = 0
            self.arr_data_first = False
        i=0
        for num in self.arr_data[num_sens]:
            i=i+int(num)
        #print(i)
        i=i/len(self.arr_data[num_sens])
        #a = i
        return i * 0.25
        #return (((a[0] << 8) | (a[1])) >> 3) * 0.25
        pass

    def printTemperature(self):
        if(self.arr_data_first == False):
            print(self.data_temperature)
    def getAllTemperature(self):
        data_device = {}
        i=0
        while i< len(self.dict_cs_obj):
            data_device["t"+str(i)]=self.getTemperature(i)
            time.sleep(0.01)
            i=i+1
        if(data_device != {}):
            self.data_temperature = ujson.dumps(data_device)
            #if(self.arr_data_first == False):
            #    print(data_device)


    #   start thread read temperature
    def StartThrReadTemperature(self):
        self.idThrReadTemperature = _thread.start_new_thread("ThrReadTemperature", self.ThrReadTemperature, ())
    #   stop thread read temperature
    def StopThrReadTemperature(self):
        if (self.idThrReadTemperature != None):
            _thread.stop(self.idThrReadTemperature)
    def ThrReadTemperature(self):
        # Поток для вызова ф-ций каждые пол секунды
        i = 0
        try:
            while i < 100:
                #self.IndProc()
                self.getAllTemperature()
                #self.printTemperature()
                #print("ThrReadTemperature")
                notification = _thread.wait(100)
                if (notification == _thread.EXIT):
                    #print("ThrReadTemperature.EXIT")
                    break
        except KeyboardInterrupt:
            print("ThrTimePolSec KeyboardInterrupt")
        except Exception:
            print("ThrTimePolSec Exception")
