using Fitomad.OpenAI.Extensions;
using Fitomad.OpenAI.Entities.Moderation;

namespace Fitomad.OpenAI.Models.Moderation;

public sealed class ModerationRequestBuilder
{
    private ModerationRequest _request = new ModerationRequest();

    public ModerationRequestBuilder WitnInput(string text)
    {
        _request.Input = text;
        return this;
    }

    public ModerationRequestBuilder WithModel(ModerationModelType model)
    {
        return WithModel(model.GetValue());
    }

    public ModerationRequestBuilder WithModel(string modelName)
    {
        _request.Model = modelName;
        return this;
    }

    public ModerationRequest Build()
    {
        if(string.IsNullOrEmpty(_request.Input))
        {
            throw new OpenAIException("You must set a text to moderate by this model");
        }

        if(string.IsNullOrEmpty(_request.Model))
        {
            _request.Model = ModerationModelType.TextModerationLatest.GetValue();
        }

        return _request;
    }
}
