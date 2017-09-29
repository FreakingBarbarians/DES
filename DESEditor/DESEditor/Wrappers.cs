using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DES;

namespace DESEditor
{
    [Serializable]
    public class ActionTemplateWrapper {
        public ActionTemplate ActionTemplate;
        public List<EffectTemplateWrapper> effects;

        public ActionTemplateWrapper() {
            effects = new List<EffectTemplateWrapper>();
        }
    }

    [Serializable]
    public class EffectTemplateWrapper {
        public EffectTemplate EffectTemplate;
        public string RawCode;
        public EffectTemplateWrapper() {

        }
    }

    [Serializable]
    public class ActionRequirementWrapper {
        public ActionRequirement ActionRequirement;
    }
}
