using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Linq;

namespace LanguageQuest.Services
{
    public class CognitiveService
    {
        VisionServiceClient visionClient = new VisionServiceClient(Helpers.Keys.CognitiveServiceKey);

        public CognitiveService()
        {
        }

        public async Task<AnalysisResult> GetImageDescription(Stream imageStream)
        {
            VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description};

            return await visionClient.AnalyzeImageAsync(imageStream, features.ToList(), null);
        }

    }
}

