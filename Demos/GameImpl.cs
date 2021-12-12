using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
using MLEM.Startup;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Demos {
    public class GameImpl : MlemGame {

        private static readonly Dictionary<string, (string, Func<MlemGame, Demo>)> Demos = new Dictionary<string, (string, Func<MlemGame, Demo>)>();
        private Demo activeDemo;
        private double fpsTime;
        private int lastFps;
        private int fpsCounter;
        private UiMetrics cumulativeMetrics;
        private TimeSpan secondCounter;

        static GameImpl() {
            Demos.Add("Ui", ("An in-depth demonstration of the MLEM.Ui package and its abilities", game => new UiDemo(game)));
            Demos.Add("Easings", ("An example of MLEM's Easings class, an adaptation of easings.net", game => new EasingsDemo(game)));
            Demos.Add("Pathfinding", ("An example of MLEM's A* pathfinding implementation in 2D", game => new PathfindingDemo(game)));
            Demos.Add("Animation and Texture Atlas", ("An example of UniformTextureAtlases, SpriteAnimations and SpriteAnimationGroups", game => new AnimationDemo(game)));
            Demos.Add("Auto Tiling", ("A demonstration of the AutoTiling class that MLEM provides", game => new AutoTilingDemo(game)));
        }

        public GameImpl() {
            this.IsMouseVisible = true;
            // print out ui metrics every second
            this.OnDraw += (g, time) => {
                this.cumulativeMetrics += this.UiSystem.Metrics;
                this.secondCounter += time.ElapsedGameTime;
                if (this.secondCounter.TotalSeconds >= 1) {
                    this.secondCounter -= TimeSpan.FromSeconds(1);
                    Console.WriteLine($"Metrics/s: {this.cumulativeMetrics}");
                    this.cumulativeMetrics = new UiMetrics();
                }
            };
        }

        protected override void LoadContent() {
            // TODO remove with MonoGame 3.8.1 https://github.com/MonoGame/MonoGame/issues/7298
            this.GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
            this.GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            this.GraphicsDeviceManager.ApplyChanges();
            base.LoadContent();

            var tex = LoadContent<Texture2D>("Textures/Test");
            this.UiSystem.Style = new UntexturedStyle(this.SpriteBatch) {
                Font = new GenericSpriteFont(LoadContent<SpriteFont>("Fonts/TestFont")),
                TextScale = 0.1F,
                PanelTexture = new NinePatch(new TextureRegion(tex, 0, 8, 24, 24), 8),
                ButtonTexture = new NinePatch(new TextureRegion(tex, 24, 8, 16, 16), 4),
                ScrollBarBackground = new NinePatch(new TextureRegion(tex, 12, 0, 4, 8), 1, 1, 2, 2),
                ScrollBarScrollerTexture = new NinePatch(new TextureRegion(tex, 8, 0, 4, 8), 1, 1, 2, 2)
            };
            this.UiSystem.AutoScaleReferenceSize = new Point(1280, 720);
            this.UiSystem.AutoScaleWithScreen = true;
            this.UiSystem.GlobalScale = 5;

            var ui = new Group(Anchor.TopLeft, Vector2.One, false);
            this.UiSystem.Add("DemoUi", ui);
            var selection = ui.AddChild(new Panel(Anchor.Center, new Vector2(100, 80), Vector2.Zero, true));

            var backButton = ui.AddChild(new Button(Anchor.TopRight, new Vector2(30, 10), "Back") {
                OnPressed = e => {
                    this.activeDemo.Clear();
                    this.activeDemo = null;
                    selection.IsHidden = false;
                    e.IsHidden = true;
                    selection.Root.SelectElement(selection.GetChildren().First(c => c.CanBeSelected));
                },
                IsHidden = true
            });

            selection.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Select the demo you want to see below using your mouse, touch input, your keyboard or a controller. Check the demos' <c CornflowerBlue><l https://github.com/Ellpeck/MLEM/tree/main/Demos>source code</l></c> for more in-depth explanations of their functionality or the <c CornflowerBlue><l https://mlem.ellpeck.de/>website</l></c> for tutorials and API documentation."));
            selection.AddChild(new VerticalSpace(5));
            foreach (var demo in Demos) {
                selection.AddChild(new Button(Anchor.AutoCenter, new Vector2(1, 10), demo.Key, demo.Value.Item1) {
                    OnPressed = e => {
                        selection.IsHidden = true;
                        backButton.IsHidden = false;
                        backButton.Root.SelectElement(backButton);

                        this.activeDemo = demo.Value.Item2.Invoke(this);
                        this.activeDemo.LoadContent();
                    },
                    PositionOffset = new Vector2(0, 1)
                });
            }

            ui.AddChild(new Paragraph(Anchor.TopLeft, 1, p => "FPS: " + this.lastFps));
        }

        protected override void DoDraw(GameTime gameTime) {
            if (this.activeDemo != null) {
                this.activeDemo.DoDraw(gameTime);
            } else {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            base.DoDraw(gameTime);

            this.fpsCounter++;
            this.fpsTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.fpsTime >= 1) {
                this.fpsTime -= 1;
                this.lastFps = this.fpsCounter;
                this.fpsCounter = 0;
            }
        }

        protected override void DoUpdate(GameTime gameTime) {
            base.DoUpdate(gameTime);
            if (this.activeDemo != null)
                this.activeDemo.Update(gameTime);
        }

    }
}