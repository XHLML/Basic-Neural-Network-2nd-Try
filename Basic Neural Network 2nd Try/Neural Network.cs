using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Neural_Network_2nd_Try
{
    class Neural_Network
    {
        private int NumOfinput, NumOfhidden, NumOfoutput;
        private double lr;
        private Matrix_Math Winput_hidden, Whidden_output;
        public Neural_Network(double lr, int input, int hidden, int output)
        {
            // Defines the basic information of a neural network: number of input nodes, hidden nodes, output nodes and learning rate
            this.NumOfinput = input;
            this.NumOfhidden = hidden;
            this.NumOfoutput = output;
            this.lr = lr;
            //creates a matrix for the weights between the input and hidden in the size of hidden*input
            this.Winput_hidden = new Matrix_Math(NumOfhidden, NumOfoutput);
            //creates a matrix for the weights between the hidden and output in the size of output*hidden
            this.Whidden_output = new Matrix_Math(NumOfoutput, NumOfhidden);

            //randomises the matirxes values from -0.5 to 0.5
            this.Winput_hidden.RandomiseSelf(5);
            this.Whidden_output.RandomiseSelf(5);

            //this.input_hidden.PrintSelf();
            //this.hidden_output.PrintSelf();
        }

        public void train()
        {

        }

        public Matrix_Math query(double[,] inputs_array)
        {
            //converts the array into a useable matrix object
            Matrix_Math inputs = new Matrix_Math(inputs_array);
            //Multiplie the input-hidden weight matrix with the inputs to create the input of the hidden layer
            Matrix_Math hidden_inputs = Matrix_Math.multiply(this.Winput_hidden, inputs);
            //Sigmoids the input of the hidden layer to create the output of the hidden layer
            Matrix_Math hidden_outputs = Matrix_Math.Map(hidden_inputs,sigmoid);
            //Multiplie the hidden-output weight matrix with the output of the hidden layer to create the input of the final layer
            Matrix_Math final_inputs = Matrix_Math.multiply(this.Whidden_output, hidden_outputs);
            //Sigmoids the input of the hidden layer to create the output of the final layer
            Matrix_Math final_outputs = Matrix_Math.Map(final_inputs, sigmoid);

            return final_outputs;
        }

        public static double sigmoid(double x, int rows, int cols)
        {
            return (Math.Pow(Math.E, x)) / (Math.Pow(Math.E, x) + 1);
        }

    }
}
