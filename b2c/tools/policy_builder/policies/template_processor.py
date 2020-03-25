from mako.template import Template
from mako.runtime import Context
from io import StringIO

class TemplateProcessor(object):
    def process(self, template_file_name, template_variables):
        template = Template(filename=template_file_name)
        print(template.render(template_variables))

    def render(self, template_file_name, template_variables):
        mytemplate = Template(filename=template_file_name)
        buf = StringIO()
        context = Context(buf, **template_variables)
        mytemplate.render_context(context)
        print(buf.getvalue())