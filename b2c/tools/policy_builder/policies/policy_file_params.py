import json

class PolicyFileParams(object):
    dct = None
    file = None

    def __init__(self, file):
        self.file = file
    
    def