/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms.Platform.Tizen;

using Calculator.Controls;
using Calculator.Tizen.Renderers;

using ElmSharp;
using System;

using TizenColor = ElmSharp.Color;
using Tizen;
using System.Threading;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(CommandButton), typeof(CommandButtonRenderer))]
namespace Calculator.Tizen.Renderers
{
    /// <summary>
    /// Calculator command button custom renderer
    /// Actually to implement command button, A image is used instead a button to display as a calculator button.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class CommandButtonRenderer : ImageRenderer//ViewRenderer<Xamarin.Forms.Image, Image>
    {
        /// <summary>
        /// A flag boolean value indicates checking a button is clicked or not.</summary>
        private bool Clicked;

        private ElmSharp.GestureLayer GestureRecognizer;
        private String ResourceDirectory = Program.AppResourcePath;
        private static TizenColor RegularColor = new TizenColor(61, 184, 204);
        private static TizenColor PressedColor = new TizenColor(34, 104, 115);

        /// <summary>
        /// Register touch event callback for the Tap, the Long Tap and the Line behaviour. </summary>
        /// <remarks>
        /// When the button is touched, This class should change the image for each touch down/up situation.
        /// Even a button touching  starts at the Tap touch down, but touch up will be happen in several situations such as the Tap, the Long Tap, the Line.
        /// </remarks>
        /// <param name="args"> A Image element changed event's argument </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> args)
        {
            base.OnElementChanged(args);

            if (Control == null ||
                Element == null)
            {
                return;
            }

            if (GestureRecognizer == null)
            {
                GestureRecognizer = new ElmSharp.GestureLayer(Control);
                GestureRecognizer.Attach(Control);
                GestureRecognizer.LongTapTimeout = 0.001;
            }

            if (args.NewElement == null)
            {
                GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.Start, null);
                GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, null);
                GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.End, null);
                GestureRecognizer.SetLineCallback(ElmSharp.GestureLayer.GestureState.End, null);
                return;
            }

            Image imageControl = Control as Image;
            CommandButton BtnElement = Element as CommandButton;
            if (BtnElement == null)
            {
                return;
            }

            imageControl.Color = RegularColor;

            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.Start, x =>
            {
                KeyDown();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetLineCallback(GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
        }

        /// <summary>
        /// A Action delegate which is restore button image as default 
        /// and execute button's Command with CommandParameter. </summary>        
        private void KeyUp()
        {
            Image imageControl = Control as Image;
            if (imageControl != null)
            {
                imageControl.Color = RegularColor;
            }

            if (Clicked)
            {
                CommandButton BtnElement = Element as CommandButton;
                BtnElement?.Command?.Execute(BtnElement.CommandParameter);
            }

            Clicked = false;
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>        
        private void KeyDown()
        {
            Clicked = true;
            Image imageControl = Control as Image;
            imageControl.Color = PressedColor;
        }
    }
}