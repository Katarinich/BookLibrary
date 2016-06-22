var path = require('path');
var webpack = require('webpack');

var CleanPlugin = require('clean-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var CommonsChunkPlugin = require("webpack/lib/optimize/CommonsChunkPlugin");

//
// defining environment vars
//

var NODE_ENV = process.env.NODE_ENV;
var APP_ENV = process.env.APP_ENV;

var isOptimizedBuild = NODE_ENV === "production";
var hotLoadEnabled =  APP_ENV === "development";

function addHotEntries(base) {
  var entries = [];

  if (hotLoadEnabled) {
    entries.push('webpack-hot-middleware/client');
  }

  entries = entries.concat(base);

  return entries;
}

//
// settings for bundles
//

var buildDir = 'build';
var targetDir = 'static/' + buildDir;
var publicPath = '/' + buildDir + '/';

//
// webpack config
//

module.exports = {
  //
  devtool: isOptimizedBuild
          ? '#cheap-module-source-map'
          : '#cheap-module-eval-source-map',
  //
  entry: {
    react: [
      'react'
      , 'react-dom'
      , 'react-bootstrap'
      , 'react-router'
    ]
    ,
    redux: [
      'redux'
      , 'react-redux'
      , 'redux-logger'
      , 'redux-thunk'
    ],
    'index' : addHotEntries([
      './index.js'
    ])
  },
  //
  output: {
    path: path.join(__dirname, targetDir),
    filename: '[name].js',
    chunkFilename: '[name]-[chunkhash].js',
    publicPath: publicPath
  },
  //
  plugins: (function() {
    var plugins = [];

    plugins.push(new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify(NODE_ENV),
      'APP_ENV': JSON.stringify(APP_ENV)
    }));

    if (isOptimizedBuild) {
      plugins.push(
        new CleanPlugin([targetDir]),
        new webpack.optimize.DedupePlugin()
      );
    }

    plugins.push(
      new webpack.optimize.OccurenceOrderPlugin(),
      new webpack.HotModuleReplacementPlugin(),
      new webpack.NoErrorsPlugin(),

      new webpack.ProvidePlugin({
        $: "jquery",
        jQuery: "jquery",
        'fetch': 'imports?this=>global!exports?global.fetch!whatwg-fetch'
      }),

      new CommonsChunkPlugin({
        name: "app",
        chunks: ["system", "index"]
      }),

      new CommonsChunkPlugin({
        names: ["libs", "react", "redux"],
        minChunks: Infinity
      }),

      new ExtractTextPlugin('[name].css', {
        allChunks: true
      })
    );

    return plugins;
  })(),
  //
  progress: true,
  //
  stats: {
    colors: true,
    reasons: true
  },
  //
  module: {
    loaders: [
      { test: /\.js$/, loaders: ['babel'], exclude: /node_modules/, include: __dirname },
      { test: /\.css$/, loader: ExtractTextPlugin.extract('style', 'css') },
      { test: /\.scss$/, loader: ExtractTextPlugin.extract('style', 'css!sass') },
      { test: /\.(png|jpg)$/, loader: 'url?limit=25000' },
      { test: /\.woff(2)?(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=application/font-woff" },
      { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=application/octet-stream" },
      { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: "file" },
      { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=image/svg+xml" },
      { test: /bootstrap\/js\//, loader: 'imports?jQuery=jquery' }
    ]
  }
};
