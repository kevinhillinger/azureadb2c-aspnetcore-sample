
from policies import FileUtil, TemplateProcessor
import os
import logging

logging.basicConfig(level=logging.DEBUG,)

def main():
    fileUtil = FileUtil()

    current_dir = os.getcwd()
    target_dir = os.path.abspath(os.path.join(current_dir, "./../b2c/policies"))

    xml_files = fileUtil.list_xml_files(path = target_dir)
    template_processor = TemplateProcessor()

    template_file="/Users/hillingk/Workspace/github.com/kevinhillinger/azureadb2c-aspnetcore-sample/b2c/policies/2-rbac/rbac.extensions.xml"
    variables = {
        "roleServiceUrl": "test",
        "tenantId": "idhack007.onmicrosoft.com",
        "policyNamePrefix": "B2C_1A"
    }
    template_processor.render(template_file, variables)