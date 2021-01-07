using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyGraphics.ViewModels;

namespace EasyGraphics.Constants
{
    public class StringConstants
    {
        public static readonly string FractalPictureName = "fractalTip";
        public static readonly string ColorSchemePictureName = "colorTip";
        public static readonly string RectangleMovePictureName = "rectangleTip";
        public static readonly string ColorSchemeHelp = "ColorsHelp";
        public static readonly string FractalsHelp = "FractalsHelp";
        public static readonly string AffinesHelp = "AffinesHelp";


        public static readonly List<string> RectangleMovePages = new List<string>
        {
            "The mathematical basis of the problem of creating moving images in computer graphics are affine transformations.",
            "Affine transformations of three types are the basis for moving images: movement / shift, scaling (enlargement / reduction), rotation at an angle.",
            "Using homogeneous coordinates and third-order matrices, we can describe an arbitrary affine transformation.",
            "Any affine transformation can be represented as a sequence of operations from among the simplest: shear, tension / compression and rotation.",
            "Almost all affine transformations depend on the order of their execution of the transformation of the turn (non-commutative)",
        };

        public static readonly List<string> FractalPages = new List<string>
        {
            "The word fractal is formed from the Latin fractus and in translation means one consisting of fragments (crushed)",
            "Main idea: an infinite number of figures in beauty and variety can be obtained from relatively simple structures with just two operations - copying and scaling.",
            "One of the main properties of fractals is self-similarity. In the simplest case, a small part of the fractal contains information about the entire fractal.",
            "Geometric and algebraic fractals are called deterministic. The subspecies of fractals formed through a system of iterative functions is IFS fractals.",
            "Fractals of this type are built in stages. First, the base is depicted. Then some parts of the base are replaced by a fragment.",
            "Iterations of nonlinear mappings given by simple algebraic formulas are used to construct algebraic fractals."
        };

        public static readonly List<string> ColorSchemePages = new List<string>
        {
            "The purpose of a color model is to provide a means of describing color within a certain color range, including to perform color interpolation.",
            "There are different models, because different actions are performed with the image: display on the screen, color processing, brightness, intensity, etc.",
            "The scheme of additive colors works on the basis of the principle of light emission. RGB is used in displays to form colors.",
            "Subtractive model CMY - hardware-oriented model used for color formation based on the principle of subtraction from the incident light flux of the part.",
            "HSV and HSL color models are used to get rid of the limitations imposed by the hardware.",
            "A color palette is a set of current colors for specific images, presented in tabular form. The palette is used if you need to save resources.",
        };

        public static readonly ObservableCollection<QuestionViewModel> Questions = new ObservableCollection<QuestionViewModel>
        {
            new QuestionViewModel
            {
                Question = "Why was a fourth component K added to the three-component CMY model?",
                Order = 1,
                Options = new ObservableCollection<string>
                {
                    "For practical purposes",
                    "Color palette optimization",
                    "To increase the brightness of colors"
                },
                CorrectAnswer = "For practical purposes"
            },
            new QuestionViewModel
            {
                Question = "What is the HSV color model used for?",
                Order = 2,
                Options = new ObservableCollection<string>
                {
                    "To switch between subtractive and additive models",
                    "When you want to adjust color attributes",
                    "To create an optimized color palette"
                },
                CorrectAnswer = "When you want to adjust color attributes"
            },
            new QuestionViewModel
            {
                Question = "Yellow in the HSV system is set by the values:",
                Order = 3,
                Options = new ObservableCollection<string>
                {
                    "(30º, 1, 1)",
                    "(60º, 1, 1)",
                    "(60º, 0, 1)"
                },
                CorrectAnswer = "(60º, 1, 1)"
            },
            new QuestionViewModel
            {
                Question = "The color palette is used for:",
                Order = 4,
                Options = new ObservableCollection<string>
                {
                    "Compact image storage",
                    "Color interpolation",
                    "Increasing the brightness of colors"
                },
                CorrectAnswer = "Compact image storage"
            },
            new QuestionViewModel
            {
                Question = "Which color model is additive?",
                Order = 5,
                Options = new ObservableCollection<string>
                {
                    "RGB",
                    "HSV",
                    "CMYK"
                },
                CorrectAnswer = "RGB"
            },
            new QuestionViewModel
            {
                Question = "Which class of fractals does not exist?",
                Order = 6,
                Options = new ObservableCollection<string>
                {
                    "IFS",
                    "Geometric",
                    "Vector"
                },
                CorrectAnswer = "Vector"
            },
            new QuestionViewModel
            {
                Question = "What space can be the result of affine transformations of 3D space?",
                Order = 7,
                Options = new ObservableCollection<string>
                {
                    "Only 3D",
                    "3D and 4D",
                    "3D and 2D"
                },
                CorrectAnswer = "Only 3D"
            },
        };
    }
}
