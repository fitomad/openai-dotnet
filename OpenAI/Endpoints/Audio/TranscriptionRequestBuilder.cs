using Fitomad.OpenAI.Entities.Audio;
using Fitomad.OpenAI.Extensions;

namespace Fitomad.OpenAI.Endpoints.Audio;

public sealed class TranscriptionRequestBuilder
{
    private TranscriptionRequest _request = new TranscriptionRequest();

    public TranscriptionRequestBuilder WithFile(string name)
    {
        _request.File = name;
        return this;
    }

    public TranscriptionRequestBuilder WithModel(TranscriptionModelType model)
    {
        return WithModel(model.GetValue());
    }

    public TranscriptionRequestBuilder WithModel(string modelName)
    {
        _request.Model = modelName;
        return this;
    }

    public TranscriptionRequestBuilder WithLanguage(string languageIsoCode)
    {
        _request.Language = languageIsoCode;
        return this;
    }

    public TranscriptionRequestBuilder WithPrompt(string prompt)
    {
        _request.Prompt = prompt;
        return this;
    }

    public TranscriptionRequestBuilder WithResponseFormat(TranscriptionResponseFormat format)
    {
        return WithResponseFormat(format.GetValue());
    }

    public TranscriptionRequestBuilder WithResponseFormat(string formatName)
    {
        _request.ResponseFormat = formatName;
        return this;
    }

    public TranscriptionRequestBuilder WithTemperature(Temperature temperature)
    {
        return WithTemperature(temperature.GetValue());
    }

    public TranscriptionRequestBuilder WithTemperature(double value)
    {
        _request.Temperatute = value;
        return this;
    }

    public TranscriptionRequest Build()
    {
        if(string.IsNullOrEmpty(_request.File))
        {
            throw new OpenAIException("A valid file is mandatory for translation operations.");
        }

        if(string.IsNullOrEmpty(_request.Model))
        {
            throw new OpenAIException("You must indicates a valid model.");
        }

        if(_request.Temperatute.IsOutOfOpenAIRange())
        {
            throw new OpenAIException("Temperature value id out of valid range.");
        }

        return _request;
    }
}