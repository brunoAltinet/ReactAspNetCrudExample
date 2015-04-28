/*!
 * Facebook React Starter Kit | https://github.com/kriasoft/react-starter-kit
 * Copyright (c) KriaSoft, LLC. All rights reserved. See LICENSE.txt
 */

/*jslint node: true */
"use strict";

var webpack = require('webpack');
var path = require('path');

/**
 * Get configuration for Webpack
 *
 * @see http://webpack.github.io/docs/configuration
 *      https://github.com/petehunt/webpack-howto
 *
 * @param {boolean} release True if configuration is intended to be used in
 * a release mode, false otherwise
 * @return {object} Webpack configuration
 */
module.exports = function(release) {
  return {
    entry: {
        app: './apps/entry.js',
        vendor: ['jquery', 'react', "reflux", "object-assign", "react-router", "jquery-ui-browserify", "q","newforms","newforms-bootstrap"]
    },

    output: {
      filename: 'bundle.js',
      path: './build/',
      publicPath: './build/',
        // export itself to a global var
      libraryTarget: "var",
        // name of the global var: "Foo"
      library: "AltiFinApp"
    },

    cache: !release,
    debug: !release,
    devtool: "eval",

    stats: {
      colors: true,
      reasons: !release
    },

    plugins: release ? [
      new webpack.DefinePlugin({
        'process.env.NODE_ENV': '"production"',
        '__DEV__': false
      }),
      new webpack.optimize.DedupePlugin(),
      new webpack.optimize.UglifyJsPlugin(),
      new webpack.optimize.OccurenceOrderPlugin(),
      new webpack.optimize.AggressiveMergingPlugin(),
      new webpack.optimize.CommonsChunkPlugin(/* chunkName= */"vendor", /* filename= */"vendor.bundle.js")
    ] : [
      new webpack.DefinePlugin({ '__DEV__': true }),
      new webpack.optimize.CommonsChunkPlugin(/* chunkName= */"vendor", /* filename= */"vendor.bundle.js")
    ],

    resolve: {
        extensions: ['', '.webpack.js', '.web.js', '.js', '.jsx'],
        alias: {
            client: path.join(__dirname, "../apps")
        }

    },

    module: {
      preLoaders: [
        {
          test: /\.js$/,
          exclude: /node_modules/,
          loader: 'jshint'
        }
      ],

      loaders: [
        {
          test: /\.css$/,
          loader: 'style!css'
        },
        {
          test: /\.less$/,
          loader: 'style!css!less'
        },
        {
          test: /\.gif/,
          loader: 'url-loader?limit=10000&mimetype=image/gif'
        },
        {
          test: /\.jpg/,
          loader: 'url-loader?limit=10000&mimetype=image/jpg'
        },
        {
          test: /\.png/,
          loader: 'url-loader?limit=10000&mimetype=image/png'
        },
        {
          test: /\.jsx?$/,
          loader: 'jsx-loader?harmony&stripTypes'
        }
      ]
    }
  };
};
