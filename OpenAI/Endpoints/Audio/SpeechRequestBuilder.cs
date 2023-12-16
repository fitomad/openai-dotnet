using Fitomad.OpenAI.Entities.Audio;

namespace Fitomad.OpenAI.Endpoints.Audio;

public sealed class SpeechRequestBuilder
{
    private SpeechRequest _request = new SpeechRequest();

    public SpeechRequestBuilder WithModel(SpeechModelType model)
    {
        return WithModel(model.GetValue());
    }

    public SpeechRequestBuilder WithModel(string model)
    {
        _request.Model = model;
        return this;
    }

    public SpeechRequestBuilder WithInput(string text)
    {
        _request.Input = text;
        return this;
    }

    public SpeechRequestBuilder WithVoice(VoiceType voice)
    {
        return WithVoice(voice.GetValue());
    }

    public SpeechRequestBuilder WithVoice(string voiceName)
    {
        _request.Voice = voiceName;
        return this;
    }

    public SpeechRequestBuilder WithResponseFormat(SpeechResponseFormat format)
    {
        _request.ResponseFormat = format.GetValue();
        return this;
    }

    public SpeechRequestBuilder WithSpeed(Speed speed)
    {
        return WithSpeed(speed.GetValue());
    }

    public SpeechRequestBuilder WithSpeed(double speedValue)
    {
        _request.Speed = speedValue;
        return this;
    }

    public SpeechRequest Build()
    {
        if(string.IsNullOrEmpty(_request.Model))
        {
            throw new OpenAIException("A model is expected.");
        }

        if(string.IsNullOrEmpty(_request.Input))
        {
            throw new OpenAIException("you must set an input to speech.");
        } 
        else if(_request.Input.Length > 4_096)
        {
            throw new OpenAIException("Input too long. Max lengt allowed 4.096 characters.");
        }

        if(string.IsNullOrEmpty(_request.Voice))
        {
            throw new OpenAIException("You must set a voice for this speech");
        }

        if(_request.Speed < 0.25 || _request.Speed > 4.0)
        {
            throw new OpenAIException("Voice speed is a value between 0.25 and 4.0");
        }

        return _request;
    }
}

