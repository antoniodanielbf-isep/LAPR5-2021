import { Inject, Service } from 'typedi';

import IPostRepo from '../services/IRepos/IPostRepo';
import { Post } from '../domain/Agregados/Posts/post';
import { PostId } from '../domain/ValueObjects/Posts/postId';

import { PostMap } from '../mappers/Posts/PostMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IPostPersistence } from '../dataschema/IPostPersistence';

@Service()
export default class PostRepo implements IPostRepo {
  private models: any;

  constructor(@Inject('postSchema') private postSchema: Model<IPostPersistence & Document>) {}

  private createBaseQuery(): any {
    return {
      where: {},
    };
  }

  public async exists(post: Post): Promise<boolean> {
    // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
    const idX = post.id instanceof PostId ? (<PostId>post.id).toValue() : post.id;

    const query = { domainId: idX };
    const postDocument = await this.postSchema.findOne(query as FilterQuery<IPostPersistence & Document>);

    return !!postDocument === true;
  }

  public async save(post: Post): Promise<Post> {
    const query = { domainId: post.id.toString() };

    const comentarioDocument = await this.postSchema.findOne(query);

    try {
      if (comentarioDocument === null) {
        const rawPost: any = PostMap.toPersistence(post);

        const postCreated = await this.postSchema.create(rawPost);

        return PostMap.toDomain(postCreated);
      } else {
        comentarioDocument.texto = post.texto.value;
        comentarioDocument.tags = post.tag.value;
        comentarioDocument.utilizador = post.utilizador;
        await comentarioDocument.save();

        return post;
      }
    } catch (err) {
      throw err;
    }
  }

  public async findById(postId: PostId | string): Promise<Post> {
    const query = { domainId: postId };
    const postRecord = await this.postSchema.findOne(query as FilterQuery<IPostPersistence & Document>);

    if (postRecord != null) {
      return PostMap.toDomain(postRecord);
    } else return null;
  }

  public async findAll(): Promise<Post[]> {
    const postRecord = await this.postSchema.find({});

    var postArray = [];
    try {
      if (postRecord === null) {
        return null;
      } else {
        for (let i = 0; i < postRecord.length; i++) {
          postArray[i] = await PostMap.toDomain(postRecord[i]);
        }
        return postArray;
      }
    } catch (err) {
      throw err;
    }
  }
}
