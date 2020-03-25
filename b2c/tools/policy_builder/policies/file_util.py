import os
import logging

class FileUtil(object):
    
    def list_xml_files(self, path):
        files = []
        # r=root, d=directories, f = files
        for r, d, f in os.walk(path):
            for file in f:
                if '.xml' in file:
                    files.append(os.path.join(r, file))

        files_found_msg=[]
        files_found_msg.append("\nFiles Found:")
        for f in files:
             files_found_msg.append("\n  " + f)
        logging.debug(''.join(files_found_msg))

        return files