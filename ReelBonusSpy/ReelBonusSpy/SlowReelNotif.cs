using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Linq.Expressions;

namespace ReelBonusSpy;

public class SlowReelNotifPatch : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var catchFishWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "Network"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "_update_chat"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "text"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
			t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf
		]);

        var newLineConsume = new TokenConsumer(t => t.Type is TokenType.Newline);

        // loop through all tokens in the script
        foreach (var token in tokens) {
			if (newLineConsume.Check(token))
			{
				continue;
			}
			else if (catchFishWaiter.Check(token)) {
                // found our match, return the original if statement
                yield return token;

				// insert custom gdscript func
				// if get_node("/root/ErythtiniReelBonusSpy").fishCatch(fish_chance, (recent_reel>0))
				yield return new IdentifierToken("get_node");
				yield return new Token(TokenType.ParenthesisOpen);
				yield return new ConstantToken(new StringVariant("/root/ErythtiniReelBonusSpy"));
				yield return new Token(TokenType.ParenthesisClose);
				yield return new Token(TokenType.Period);
				yield return new IdentifierToken("fishCatch");
				yield return new Token(TokenType.ParenthesisOpen);
				yield return new IdentifierToken("fish_chance");
				yield return new Token(TokenType.Comma);
				yield return new Token(TokenType.ParenthesisOpen);
				yield return new IdentifierToken("recent_reel");
				yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new RealVariant(0));
				yield return new Token(TokenType.ParenthesisClose);
				yield return new Token(TokenType.ParenthesisClose);

				// remove remaining tokens up to colon
				newLineConsume.SetReady();
				yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
			} else {
                // return the original token
                yield return token;
            }
        }
    }
}
